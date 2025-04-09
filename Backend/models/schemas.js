const mongoose = require("mongoose");

const userSchema = new mongoose.Schema({
  uid: { type: String, required: true, unique: true }, // firebase UID
  gameID: { type: String, unique: true, default: generateGameID },
  username: { type: String, required: true, unique: true },
  level: { type: Number, default: 1 },
  xp: { type: Number, default: 0 },
  wins: { type: Number, default: 0 },
  profile_icon: { type: Number, default: 0 },
  streak_count: { type: Number, default: 0 },
  friends: [{ type: String }],
  friendRequests: [{ type: String }],
  sentRequests: [{ type: String }],
  achievements: [
    {
      name: { type: String, required: true },
      description: { type: String, required: true },
      progress: { type: Number, default: 0 },
      target: {type: Number, required: true}
    }
  ]
});

const User = mongoose.model("User", userSchema);
module.exports = User;

function generateGameID() {
  return Math.floor(100000 + Math.random() * 900000); // 6-digit ID
}