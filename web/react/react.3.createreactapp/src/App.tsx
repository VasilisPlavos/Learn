import { useState } from "react";
import "./App.css";

function MyButton() {
  const [count, countSet] = useState(0);

  function increment(): void {
    countSet(count + 1);
  }

  return <button onClick={increment}>{count} clicks!</button>;
}

function DependentButton({ count, onClick }: any) {
  return <button onClick={onClick}>{count} clicks!</button>;
}

function DependentButtons({ quantity }: any) {
  const [count, countSet] = useState(0);

  function increment(): void {
    countSet(count + 1);
  }

  var elements: JSX.Element[] = [];
  for (let index = 0; index < quantity; index++) {
    elements.push(
      <DependentButton key={index} count={count} onClick={increment} />
    );
  }

  return (
    <div>
      {elements}
      <hr />
      <DependentButton count={count} onClick={increment} />
      <DependentButton count={count} onClick={increment} />
    </div>
  );
}

function DynInput(){
  const [value, valueSet] = useState('enter input');

  function updateValue(event: any) {
    console.log(event);
    valueSet(`${event.target.value}`);
  }

  return (
    <div>
      <hr />
      <h2>Dynamic Input</h2>
      <input type="text" onChange={updateValue} />
      <p>{value}</p>
    </div>
  )
}

function Clock() {
  const [date, dateSet] = useState(new Date().toLocaleTimeString());
  setInterval(() => {
    dateSet(new Date().toLocaleTimeString());
  }, 1000);

  return (
    <div>
      <h2>Clock</h2>
      <h3>It's {date}</h3>
    </div>
  );
}

function NewHello(props: any) {
  console.log(props);
  return <div>{JSON.stringify(props)}</div>;
}

function App() {
  var hello = "Hello world";
  var user = { fname: "John", lname: "Doe" };

  return (
    <div className="App">
      <h1>{hello}</h1>
      <NewHello props={user} />
      <NewHello username={"nio"} age={4} />
      <DynInput />
      <h2>Independent buttons</h2>
      <MyButton />
      <MyButton />
      <h2>Dependent buttons</h2>
      <DependentButtons quantity={3} />
      <Clock />
    </div>
  );
}

export default App;
