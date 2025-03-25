import { useState } from "react";
import { FlatList, Text, TouchableOpacity, View } from "react-native";

const NotesScreen = () => {

    const [notes, setNotes] = useState([
        { id: "1", text: "Note one" },
        { id: "2", text: "Note two" },
        { id: "3", text: "Note three" },
    ])
    

    return (
        <View>
            <FlatList
                data={notes}
                keyExtractor={(i) => i.id}
                renderItem={({ item }) => (
                    <View>
                        <Text>{item.text}</Text>
                    </View>
                )} />

                <TouchableOpacity>
                    <Text>+ Add Note</Text>
                </TouchableOpacity>
        </View>
    );
}


export default NotesScreen;