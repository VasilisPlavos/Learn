"use client"

import { useState, useEffect } from "react";

export default function MainContent() {

    const [title, setTitle] = useState("");
    const [count, setCount] = useState(0);

    useEffect(() => {
        fetch("https://jsonplaceholder.typicode.com/todos/1")
            .then((x) => x.json())
            .then((x) => setTitle(x.title));
    }, [
        count // every time that count change, this useEffect is triggered
    ]);

    useEffect(() => {
        document.title = `Count ${count}`;
    }, [
        count, // comment this
    ]);


    return (
        <>
            <br />
            <div className="items-center  justify-items-center list-inside list-decimal text-sm text-center sm:text-left font-[family-name:var(--font-geist-mono)]">
                <h1>useEffect example</h1>
                <h2>{title}</h2>
                <div>
                    <p> Count: {count} </p>
                    <button
                        className="rounded-full border border-solid border-transparent transition-colors flex items-center justify-center bg-foreground text-background gap-2 hover:bg-[#383838] dark:hover:bg-[#ccc] text-sm sm:text-base h-10 sm:h-12 px-4 sm:px-5"
                        onClick={() => setCount(count + 1)}>+</button>
                </div>
            </div>
        </>
    )
}