const express = require("express");
const cors = require("cors");
const admin = require("firebase-admin");
const { MongoClient } = require("mongodb");
require("dotenv").config();

const app = express();
app.use(cors()); // Allow requests from Unity
app.use(express.json());

// Initialize Firebase Admin SDK
const serviceAccount = require("./firebase-adminsdk.json");
admin.initializeApp({
  credential: admin.credential.cert(serviceAccount),
});

// MongoDB Connection
const mongoUri = process.env.MONGO_URI;
const client = new MongoClient(mongoUri, {
  useNewUrlParser: true,
  useUnifiedTopology: true,
  tls: true,
  tlsAllowInvalidCertificates: true, 
});

async function connectDB() {
  try {
    await client.connect();
    console.log("Connected to MongoDB...");
  } catch (error) {
    console.error("MongoDB Connection Error:", error);
    process.exit(1);
  }
}

connectDB();
const db = client.db("Users");
const usersCollection = db.collection("users");

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

// 🔹 User Data Route (Auto-creates user if missing)
app.get("/userdata", authenticateToken, async (req, res) => {
  try {
    const uid = req.user.uid;
    const email = req.user.email;

    let user = await usersCollection.findOne({ uid });

    if (!user) {
      user = {
        uid: uid,
        email: email,
        username: email.split("@")[0],
        createdAt: new Date(),
      };
      await usersCollection.insertOne(user);
      console.log("New user added to MongoDB:", user);
    }

    res.json({ email: req.user.email, data: user });
  } catch (error) {
    console.error("Error fetching user data:", error);
    res.status(500).json({ error: error.message });
  }
});

// 🔹 Start the Server
const PORT = 3000;
app.listen(PORT, () => console.log(`Server running on http://localhost:${PORT}`));