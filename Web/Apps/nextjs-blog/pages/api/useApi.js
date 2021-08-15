export default async function handler(req, res) {
  var people = await fetch(`https://ghibliapi.herokuapp.com/people`)
    .then((response) => { return response.json(); });

  res.status(200).json(people);
}
