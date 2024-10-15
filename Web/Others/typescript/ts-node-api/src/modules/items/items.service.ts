/**
 * Data Model Interfaces
 */

import { Item } from "./item.model";

/**
 * In-Memory Store
 */
let items: Item[] =
    [
        {
            id: 564,
            name: "Burger",
            price: 599,
            description: "Tasty",
            image: "https://cdn.auth0.com/blog/whatabyte/burger-sm.png"
        },
        {
            id: 437,
            name: "Pizza",
            price: 299,
            description: "Cheesy",
            image: "https://cdn.auth0.com/blog/whatabyte/pizza-sm.png"
        },
        {
            id: 356,
            name: "Tea",
            price: 199,
            description: "Informative",
            image: "https://cdn.auth0.com/blog/whatabyte/tea-sm.png"
        }
    ];


/**
 * Service Methods
 */
export const findAllAsync = async (): Promise<Item[]> => items;
export const findByIdAsync = async (id: number): Promise<Item | undefined> => items.find(x => x.id === id);

export const createAsync = async (item: Item): Promise<Item> => {
    item.id = new Date().valueOf();
    items.push(item);
    return item;
}

export const updateAsync = async (item: Item): Promise<void> => {
    let index = items.findIndex(x => x.id === item.id);
    if (index < 0) throw new Error("Item not found");
    items[index] = item;
}

export const removeAsync = async (itemId: number): Promise<void> => {
    items = items.filter(x => x.id != itemId);
}