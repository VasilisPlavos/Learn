import { useState } from "react";
import "./App.css";

function MyButton() {
  const [count, countSet] = useState(0);

  function increment(): void {
    countSet(count + 1);
  }

  return <button onClick={increment}>{count} clicks!</button>;
}

function App() {
  return (
    <div className="App">
      <h1>Counter</h1>
      <MyButton />
      <MyButton />
    </div>
  );
}

export default App;
