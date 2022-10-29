import { User } from "../data/database.mjs";
import bcrypt from "bcrypt";

async function authenticateUserAsync(username, password) {
  if (!username || !password) {
    console.log("username or password is empty");
    return null;
  }

  let user = await User.findOne({ where: { username: username } });
  if (!user) {
    console.log("error: user not exist");
    return null;
  }

  const match = await bcrypt.compare(password, user.passwordHashed);
  if (!match) {
    console.log("pass not match");
    return null;
  }

  console.log("login", user);
  return user;
}

async function createAsync(username, password) {
  if (!username || !password) {
    console.log("username or password is empty");
    return null;
  }

  let user = await User.findOne({ where: { username: username } });
  if (user) {
    console.log("user exist");
    return null;
  }

  const hashedPassword = await bcrypt.hash(password, 10);
  user = await User.create({
    username: username,
    passwordHashed: hashedPassword,
  });
  console.log("created", user);
  return user;
}

export { authenticateUserAsync, createAsync };
