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
    const user = new User({ uid, username });
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



// Start the Server
const PORT = 3000;
app.listen(PORT, () => console.log(`Server running on http://localhost:${PORT}`));
