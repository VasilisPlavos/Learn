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

function DependentButtons() {
  const [count, countSet] = useState(0);

  function increment(): void {
    countSet(count + 1);
  }

  return (
    <div>
      <DependentButton count={count} onClick={increment} />
      <DependentButton count={count} onClick={increment} />
    </div>
  );
}

function App() {
  return (
    <div className="App">
      <h1>Counter</h1>
      <h2>Independent buttons</h2>
      <MyButton />
      <MyButton />
      <h2>Dependent buttons</h2>
      <DependentButtons />
    </div>
  );
}

export default App;
