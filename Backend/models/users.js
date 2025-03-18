const mongoose = require("mongoose");

// Define User Schema
const userSchema = new mongoose.Schema({
  uid: { type: String, required: true, unique: true }, // Firebase UID
  username: { type: String, required: true, unique: true }, // Unique username
  level: { type: Number, default: 1 }, // Default level 1
  xp: { type: Number, default: 0 }, // Default XP 0
  wins: { type: Number, default: 0 }, // Default Wins 0
  profile_icon: { type: Number, default: 0 }, // Default Profile Icon
  streak_count: { type: Number, default: 0 }, // Default Streak Count 0
  createdAt: { type: Date, default: Date.now }, // Timestamp
});

// Create and export the User model
const User = mongoose.model("User", userSchema);
module.exports = User;
