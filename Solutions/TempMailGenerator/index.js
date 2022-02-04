document.querySelector("#thisYear").innerText = new Date().getFullYear();
document.querySelector("#serverAlert").style.visibility = "hidden";

async function getDomainAsync() {
  var response = await axios.get("https://api.mail.tm/domains?page=1");
  return response.data["hydra:member"][0].domain;
}

async function createEmailAsync() {
  var domain = await getDomainAsync();
  var username =
    Math.random().toString(36).substring(2, 15) +
    Math.random().toString(36).substring(2, 15);
  const user = { address: `${username}@${domain}`, password: `${username}` };
  var response = await axios.post("https://api.mail.tm/accounts", user);
  if (response.status == 201) return user;
  return null;
}

var user = await createEmailAsync();
if (user) {
  document.querySelector("#emailAddress").value = `${user.address}`;
  document.querySelector("#password").value = `${user.password}`;
  document.querySelector("body > main > form > img").style.visibility = "visible";
  document.querySelector("body > main > form > h1").innerText = "Account created successfully";
}
