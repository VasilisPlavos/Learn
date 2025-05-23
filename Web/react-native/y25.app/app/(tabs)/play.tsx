import {View} from "react-native";
import {HelloWave} from "@/components/HelloWave";
import React, {useState} from "react";

const user = {
    name: "John",
    imageUrl: 'https://i.imgur.com/yXOvdOSs.jpg',
    imageSize: 90
}

const isLoggedIn: boolean = true;
const products = [
    { title: 'Cabbage', id: 1 },
    { title: 'Garlic', id: 2 },
    { title: 'Apple', id: 3 },
];

const listItems = products.map(x => <li key={x.id}>{x.title}</li>);


export default function Play() {
    const [count, setCount] = useState(0);

    function handleClick() {
        setCount(count + 1);
    }

    return (
        <>
            <View>
                <ul>{listItems}</ul>

                <h1>{user.name}</h1>
                <img
                    className={'avatar'}
                    src={user.imageUrl}
                    alt="{`Photo of ${user.name}`}"
                    style={{ width: user.imageSize, height: user.imageSize }}
                />
                <CounterButton/>
                < CounterButton/>
                <SyncCounter count={count} onClick={handleClick} />
                <SyncCounter count={count} onClick={handleClick} />
            </View>
            {isLoggedIn && <HelloWave/>}
        </>
    )
}

function SyncCounter({ count, onClick } : { count: number, onClick: () => void }) {
    return (<button onClick={onClick}> Sync clicked: {count} </button>)
}


function CounterButton() {
    const [count, setCount] = useState(0);

    function handleClick() {
        setCount(count + 1);
    }

    return (<button onClick={() => {
        handleClick()
    }}> Clicked {count} times </button>)
}
