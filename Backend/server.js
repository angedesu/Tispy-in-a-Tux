require("dotenv").config();
const express = require("express");
const cors = require("cors");
const admin = require("firebase-admin");
const mongoose = require("mongoose");
const User = require("./models/schemas.js"); // Import models

const app = express();
app.use(cors()); // Allow requests from Unity
app.use(express.json());

// Initialize Firebase Admin SDK
const serviceAccount = require("./firebase-adminsdk.json");
admin.initializeApp({
  credential: admin.credential.cert(serviceAccount),
});

// Connect to MongoDB
const mongoUri = process.env.MONGO_URI;
mongoose.connect(mongoUri, {
  useNewUrlParser: true,
  useUnifiedTopology: true
})
  .then(() => console.log("Connected to MongoDB!"))
  .catch(err => console.error("MongoDB Connection Error:", err));

// Middleware: Verify Firebase ID Token
async function authenticateToken(req, res, next) {
  console.log("📡 Incoming Headers:", req.headers);
  const token = req.headers.authorization?.split("Bearer ")[1];

  if (!token) {
    return res.status(401).json({ error: "Unauthorized - No Token" });
  }

  try {
    const decodedToken = await admin.auth().verifyIdToken(token);
    console.log("Token Verified:", decodedToken.uid);
    req.user = decodedToken;
    next();
  } catch (error) {
    console.log("Token Verification Failed:", error.message);
    res.status(403).json({ error: "Unauthorized - Invalid Token" });
  }
}

// Register User
app.post("/register", authenticateToken, async (req, res) => {
  try {
//    console.log("Received /register request");

    const { username } = req.body;
    const uid = req.user.uid;

    // Check if username already exists
    const existingUser = await User.findOne({ username });
    if (existingUser) {
      return res.status(400).json({ error: "Username already taken" });
    }

    // Create new user
    const user = new User({ 
      uid: uid, 
      username: username,
      achievements: ACHIEVEMENTS.map(ach => ({
        name: ach.name,
        description: ach.description,
        progress: 0,
        target: ach.target
      }))
    
    });
    await user.save();

    console.log("User registered:", user);
    res.json({ message: "User registered successfully!", user });
  } catch (error) {
    console.error("Error registering user:", error);
    res.status(500).json({ error: error.message });
  }
});

// Fetch Full User Data
app.get("/userdata", authenticateToken, async (req, res) => {
  try {
    const user = await User.findOne({ uid: req.user.uid });

    if (!user) {
      return res.status(404).json({ error: "User not found" });
    }

    res.json({ user });
  } catch (error) {
    console.error("Error fetching user data:", error);
    res.status(500).json({ error: error.message });
  }
});

// checks if username exists
app.get('/check-username', async (req, res) => {
  const username = req.query.username;
  if (!username) return res.status(400).json({ error: "Missing username" });

  try {
      const user = await User.findOne({ username });

      if (user) {
          return res.status(200).json({ exists: true });
      } else {
          return res.status(200).json({ exists: false });
      }
  } catch (err) {
      console.error(err);
      return res.status(500).json({ error: "Server error" });
  }
});


// Static achievements
const ACHIEVEMENTS = [
  {name: "Welcome!", description: "Launch the game for the first time", target: 1},
  {name: "DJ", description: "Change the sound settings", target: 1},
  {name: "Power of friendship", description: "Send a friend request", target: 1},
  {name: "Avatar", description: "Change your profile picture", target: 1}
]

// View user achivements
app.get('/achievements/:gameID', async (req, res) => {
  try {
    const user = await User.findOne({ gameID: req.params.gameID }, 'achievements');

    if (!user) return res.status(404).json({ error: "User not found" });

    res.json({ achievements: user.achievements });

  } catch (error) {
    console.error("Error fetching user achievements:", error);
    res.status(500).json({ error: error.message });
  }
});

// Update user achivement progress
app.put('achievements/:gameID/:achivementName', async (req, res) => {
  const { gameID, achievementName } = req.params;
  const { newProgress } = req.body;

  try {
    const user = await User.findOne({ gameID: gameID });
    if (!user) return res.status(404).json({ error: "User not found" });

    const achivementToUpdate = user.achievements.find(ach => ach.name == achievementName);
    if (!achivementToUpdate) return res.status(404).json({ error: "Achievement not found" });

    achivementToUpdate.progress = newProgress;
    await user.save();

    res.status(200).json({ message: "Achievement progress updated successfully" });


  } catch (error) {
    console.error("Error updating achievement progress:", error);
    res.status(500).json({ error: error.message });
  }

});

// views users friends
app.get('/friends/:gameID', async (req, res) => {
  try {
    const user = await User.findOne({ gameID: req.params.gameID });

    if (!user) return res.status(404).send("User not found");

    // populate usernames of friends
    const friends = await User.find({ gameID: { $in: user.friends } }, 'username gameID level');

    res.json(friends);
  } catch (err) {
    res.status(500).send(err.message);
  }
});

// view the friend request received
app.get('/received-requests/:gameID', async (req, res) => {
  try {
    const user = await User.findOne({ gameID: req.params.gameID });

    if (!user) return res.status(404).send("User not found");

    const received = await User.find(
      { gameID: { $in: user.friendRequests } },
      'username gameID'
    );

    res.json(received);
  } catch (err) {
    res.status(500).send(err.message);
  }
});

// view all users that are NOT friends or in friend requests
app.get('/non-friends/:gameID', async (req, res) => {
  try {
    const currentUser = await User.findOne({ gameID: req.params.gameID });

    if (!currentUser) return res.status(404).send("User not found");

    const excludedGameIDs = [
      currentUser.gameID,
      ...currentUser.friends,
      ...currentUser.sentRequests,
      ...currentUser.friendRequests
    ];

    // Find users NOT in the excluded list
    const nonFriends = await User.find(
      { gameID: { $nin: excludedGameIDs } },
      'username gameID level'
    );

    res.json(nonFriends);
  } catch (err) {
    console.error("Error fetching non-friends:", err);
    res.status(500).send(err.message);
  }
});
 
// user sends friend request to a user
app.post('/send-friend-request', async (req, res) => {
  const { fromGameID, toGameID } = req.body;

  try {
    const sender = await User.findOne({ gameID: fromGameID });
    const receiver = await User.findOne({ gameID: toGameID });

    if (!sender || !receiver) return res.status(404).send("User not found");

    if (receiver.friendRequests.includes(fromGameID)) {
      return res.status(400).send("Friend request already sent");
    }

    receiver.friendRequests.push(fromGameID);
    sender.sentRequests.push(toGameID);

    await receiver.save();
    await sender.save();

    res.send("Friend request sent");
  } catch (err) {
    res.status(500).send(err.message);
  }
});

// accepts friend request if they are in the array
app.post('/accept-friend-request', async (req, res) => {
  const { fromGameID, toGameID } = req.body;

  try {
    const receiver = await User.findOne({ gameID: toGameID });
    const sender = await User.findOne({ gameID: fromGameID });

    if (!receiver || !sender) return res.status(404).send("User not found");

    if (!receiver.friendRequests.includes(fromGameID)) {
      return res.status(400).send("No pending request from this user");
    }

    // Add each other as friends
    receiver.friends.push(fromGameID);
    sender.friends.push(toGameID);

    // Remove request entries
    receiver.friendRequests = receiver.friendRequests.filter(id => id.toString() !== fromGameID.toString());
    sender.sentRequests = sender.sentRequests.filter(id => id.toString() !== toGameID.toString());


    await receiver.save();
    await sender.save();

    res.send("Friend request accepted");
  } catch (err) {
    res.status(500).send(err.message);
  }
});

// delete friend from friends array
app.post('/delete-friend', async (req, res) => {
  const userGameID = req.body.userGameID?.toString().trim();
  const friendGameID = req.body.friendGameID?.toString().trim();

  try {
    const user = await User.findOne({ gameID: userGameID });
    const friend = await User.findOne({ gameID: friendGameID });

    user.friends = user.friends.filter(id => id !== friendGameID);
    friend.friends = friend.friends.filter(id => id !== userGameID);

    await user.save();
    await friend.save();

    res.send("Friend removed");
  } catch (err) {
    console.error("Delete friend error:", err);
    res.status(500).send(err.message);
  }
});

// rejecting friend request
app.post('/reject-friend-request', async (req, res) => {
  const { fromGameID, toGameID } = req.body;

  try {
    const receiver = await User.findOne({ gameID: toGameID }); // The user who received the request
    const sender = await User.findOne({ gameID: fromGameID }); // The one who sent the request

    if (!receiver || !sender) return res.status(404).send("User not found");

    // Make sure the request actually exists
    if (!receiver.friendRequests.includes(fromGameID)) {
      return res.status(400).send("No such friend request to reject");
    }

    // Remove the friend request
    receiver.friendRequests = receiver.friendRequests.filter(id => id !== fromGameID);
    sender.sentRequests = sender.sentRequests.filter(id => id !== toGameID);

    await receiver.save();
    await sender.save();

    res.send("Friend request rejected");
  } catch (err) {
    res.status(500).send(err.message);
  }
});


// // get the streak_counter variable 
// app.get('/streak-counter/:gameID', async (req, res) => {
//   try{
//     const currentUser = await User.findOne({gameid: req.params.gameid});
    
//     if (!currentUser) return res.status(404).send("User not found");
//     res.json({streak_counter: currentUser.streak_counter});
//   }
//   catch (err) {
//     console.error("Error fetching streak counter:", err);
   
// // update streak_counter
// app.patch('/streak-counter/:gameID', async (req, res) => {
//   try{
//     const {streak} = req.body;

//     const updatedUser = await User.findOneAndUpdate(
//         {gameID: req.params.gameID},  // find the right user
//         {$set: {streak_counter: streak}},   // only update the streak
//         {new: true}                   // return the updated user
//     );

//     if (!updatedUser) return res.status(404).send("User not found");

//     res.json(updatedUser);
//   }
//   catch (err) {
//     console.error("Error fetching streak counter:", err);
//     res.status(500).send(err.message);
//   }
// });


// Start the Server
const PORT = 3000;
app.listen(PORT, () => console.log(`Server running on http://localhost:${PORT}`));
