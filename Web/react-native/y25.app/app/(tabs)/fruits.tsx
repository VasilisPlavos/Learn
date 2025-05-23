import {View} from "react-native";
import React, {useState} from "react";

const PRODUCTS = [
    { category: "Fruits", price: "$1", stocked: true, name: "Apple" },
    { category: "Fruits", price: "$1", stocked: true, name: "Dragonfruit" },
    { category: "Fruits", price: "$2", stocked: false, name: "Passionfruit" },
    { category: "Vegetables", price: "$2", stocked: true, name: "Spinach" },
    { category: "Vegetables", price: "$4", stocked: false, name: "Pumpkin" },
    { category: "Vegetables", price: "$1", stocked: true, name: "Peas" }
];

function ProductTable({ filterText, inStockOnly, products } : { filterText: string; inStockOnly: boolean; products: any }) {

    // const rows = products.map((x:any) => (
    //     <tr key={x.name}>
    //         <th>{x.name}</th>
    //         <th>{x.price}</th>
    //     </tr>
    // ));

    console.log(filterText);
    const rows: React.JSX.Element[] = [];
    products.forEach((product: any) => {

        if (product.name.toLowerCase()
            .indexOf(filterText.toLowerCase()) === -1) return;

        if (inStockOnly && !product.stocked) return;

        rows.push(
            <tr key={product.name}>
                <th>{product.name}</th>
                <th>{product.price}</th>
            </tr>
        )
    })


    return (
        <table>
            <thead>
            <tr>
                <th>Name</th>
                <th>Price</th>
            </tr>
            </thead>
            <tbody>
            {rows}
            </tbody>
        </table>
    )
}

interface SearchBarProps {
    filterText: string,
    setFilterText : React.Dispatch<React.SetStateAction<string>>,
    inStockOnly: boolean,
    setInStockOnly: React.Dispatch<React.SetStateAction<boolean>>,
}

function SearchBar(props: {   searchBarProps: SearchBarProps }) {
    const { inStockOnly, setInStockOnly, setFilterText, filterText } = props.searchBarProps;
    return (
        <form>
            <input type="text" value={filterText} placeholder="Search..."
                   onChange={(e) => setFilterText(e.target.value)}
            />
            <label>
                <input checked={inStockOnly}
                       onChange={(e) => setInStockOnly(e.target.checked)}
                       type="checkbox"/>
                {' '}
                Only show products in stock
            </label>
        </form>
    );
}

export default function Fruits() {
    const [filterText, setFilterText] = useState('');
    const [inStockOnly, setInStockOnly] = useState(false);

    return (
        <View>
            <div>
                <SearchBar searchBarProps={{ filterText, setFilterText, inStockOnly, setInStockOnly }} />
                <ProductTable filterText={filterText} inStockOnly={inStockOnly}  products={PRODUCTS}/>
            </div>
        </View>

    );
}
