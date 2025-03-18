require("dotenv").config();
const express = require("express");
const cors = require("cors");
const admin = require("firebase-admin");
const mongoose = require("mongoose");
const User = require("./models/schemas.js"); // Import models

const app = express();
app.use(cors()); // Allow requests from Unity
app.use(express.json());

// ✅ Initialize Firebase Admin SDK
const serviceAccount = require("./firebase-adminsdk.json");
admin.initializeApp({
  credential: admin.credential.cert(serviceAccount),
});

// ✅ Connect to MongoDB
const mongoUri = process.env.MONGO_URI;
mongoose.connect(mongoUri, {
  useNewUrlParser: true,
  useUnifiedTopology: true
})
  .then(() => console.log("✅ Connected to MongoDB"))
  .catch(err => console.error("❌ MongoDB Connection Error:", err));

// 🔹 Middleware: Verify Firebase ID Token
async function authenticateToken(req, res, next) {
  console.log("📡 Incoming Headers:", req.headers);
  const token = req.headers.authorization?.split("Bearer ")[1];

  if (!token) {
    return res.status(401).json({ error: "Unauthorized - No Token" });
  }

  try {
    const decodedToken = await admin.auth().verifyIdToken(token);
    console.log("✅ Token Verified:", decodedToken.uid);
    req.user = decodedToken;
    next();
  } catch (error) {
    console.log("❌ Token Verification Failed:", error.message);
    res.status(403).json({ error: "Unauthorized - Invalid Token" });
  }
}

// 🔹 Register User and Assign Achievements
app.post("/register", authenticateToken, async (req, res) => {
  try {
    console.log("📡 Received /register request");

    const { username } = req.body;
    const uid = req.user.uid;

    if (!username) {
      return res.status(400).json({ error: "Username is required" });
    }

    // Check if username already exists
    const existingUser = await User.findOne({ username });
    if (existingUser) {
      return res.status(400).json({ error: "Username already taken" });
    }

    // Default achievements list
    const defaultAchievements = [
      { name: "First Drink", description: "Make your first drink!", target: 1 },
      { name: "Winner", description: "Win 1 game in Competitve Mode", target: 1 },
      { name: "Mix Master", description: "Make 100 drinks!", target: 100 }
    ].map(a => ({ ...a, status: "NOT_ACHIEVED", progress: 0 }));

    // Create new user with default achievements
    const user = new User({ uid, username, achievements: defaultAchievements });
    await user.save();

    console.log("✅ User registered:", user);
    res.json({ message: "User registered successfully!", user });
  } catch (error) {
    console.error("❌ Error registering user:", error);
    res.status(500).json({ error: error.message });
  }
});

// 🔹 Fetch User Achievements
app.get("/user-achievements", authenticateToken, async (req, res) => {
  try {
    const user = await User.findOne({ uid: req.user.uid });

    if (!user) {
      return res.status(404).json({ error: "User not found" });
    }

    res.json({ achievements: user.achievements });
  } catch (error) {
    console.error("❌ Error fetching achievements:", error);
    res.status(500).json({ error: error.message });
  }
});

// 🔹 Update User Achievement Progress
app.post("/update-achievement", authenticateToken, async (req, res) => {
  try {
    const { achievement_name, progress } = req.body;

    if (!achievement_name || progress == null) {
      return res.status(400).json({ error: "Missing required fields" });
    }

    const user = await User.findOne({ uid: req.user.uid });

    if (!user) {
      return res.status(404).json({ error: "User not found" });
    }

    const achievement = user.achievements.find(a => a.name === achievement_name);
    if (!achievement) {
      return res.status(404).json({ error: "Achievement not found" });
    }

    // Update progress
    achievement.progress += progress;

    // Mark as "ACHIEVED" if target is met
    if (achievement.progress >= achievement.target) {
      achievement.status = "ACHIEVED";
      achievement.progress = achievement.target;
    }

    user.save();
    console.log("✅ Achievement updated:", achievement);

    res.json({ message: "Achievement updated!", achievement });
  } catch (error) {
    console.error("❌ Error updating achievement:", error);
    res.status(500).json({ error: error.message });
  }
});

// 🔹 Start the Server
const PORT = 3000;
app.listen(PORT, () => console.log(`🚀 Server running on http://localhost:${PORT}`));
