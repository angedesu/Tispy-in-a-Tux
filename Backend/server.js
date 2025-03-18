require("dotenv").config();
const express = require("express");
const cors = require("cors");
const admin = require("firebase-admin");
const mongoose = require("mongoose");
const User = require("./models/users.js");

const app = express();
app.use(cors()); // Allow requests from Unity
app.use(express.json());

// Initialize Firebase Admin SDK
const serviceAccount = require("./firebase-adminsdk.json");
admin.initializeApp({
  credential: admin.credential.cert(serviceAccount),
});

// ✅ Connect to MongoDB with Mongoose
const mongoUri = process.env.MONGO_URI

mongoose.connect(mongoUri, {
  useNewUrlParser: true,
  useUnifiedTopology: true
}).then(() => console.log("✅ Connected to MongoDB"))
  .catch(err => console.error("❌ MongoDB Connection Error:", err));

// 🔹 Middleware: Verify Firebase ID Token
async function authenticateToken(req, res, next) {
    console.log("📡 Incoming Headers:", req.headers);

    const token = req.headers.authorization?.split("Bearer ")[1];

    if (!token) {
      console.log("No token received");
      return res.status(401).json({ error: "Unauthorized - No Token" });
    }

    console.log("Received Token:", token.substring(0, 50) + "...");

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

// 🔹 Register User in MongoDB
app.post("/register", authenticateToken, async (req, res) => {
  try {
    console.log("📡 Received /register request");
    console.log("🔹 Request Headers:", req.headers);
    console.log("🔹 Request Body:", req.body); // ✅ Log request body

    const { username } = req.body;
    const uid = req.user.uid;
    const email = req.user.email;

    if (!username) {
      console.log("❌ Missing username in request!");
      return res.status(400).json({ error: "Username is required" });
    }

    // Check if username already exists
    const existingUser = await User.findOne({ username });
    if (existingUser) {
      console.log("❌ Username already exists:", username);
      return res.status(400).json({ error: "Username already taken" });
    }

    // Create new user
    const user = new User({
      uid: uid,
      username: username,
      level: 1,
      xp: 0,
      wins: 0,
      profile_icon: 0,
      streak_count: 0,
    });

    await user.save();
    console.log("✅ User saved:", user);

    res.json({ message: "User registered successfully!", user });
  } catch (error) {
    console.error("❌ Error saving user:", error);
    res.status(500).json({ error: error.message });
  }
});

// 🔹 Start the Server
const PORT = 3000;
app.listen(PORT, () => console.log(`Server running on http://localhost:${PORT}`));
