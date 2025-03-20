const mongoose = require("mongoose");

// ✅ Define User Schema
const userSchema = new mongoose.Schema({
  uid: { type: String, required: true, unique: true }, // Firebase UID
  username: { type: String, required: true, unique: true },
  level: { type: Number, default: 1 },
  xp: { type: Number, default: 0 },
  wins: { type: Number, default: 0 },
  profile_icon: { type: Number, default: 0 },
  streak_count: { type: Number, default: 0 },
  achievements: [
    {
      name: { type: String, required: true }, // Achievement Name
      description: { type: String, required: true }, // Description
      status: { type: String, enum: ["NOT_ACHIEVED", "ACHIEVED"], default: "NOT_ACHIEVED" },
      progress: { type: Number, default: 0 }, // User's progress
      target: { type: Number, required: true }, // Required progress to complete
      updated_at: { type: Date, default: Date.now }
    }
  ]
});

// ✅ Create and Export User Model
const User = mongoose.model("User", userSchema);
module.exports = User;
