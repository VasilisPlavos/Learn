import { useEffect, useState } from "react";
import { Alert, FlatList, Platform, StyleSheet, Text, TouchableOpacity, View } from "react-native";
import NoteList from "../components/NoteList";
import AddNoteModal from "../components/AddNoteModal";
import { Note } from '../models/note.interface';
import noteService from "../services/noteService";

const NoteScreen = () => {

    const [modalVisible, setModalVisible] = useState(false);
    const [newNote, setNewNote] = useState('');

    const [notes, setNotes] = useState(new Array<Note>())

    async function someRandomMethodsAsync() {
        console.log(process.env.EXPO_PUBLIC_KEY_EXAMPLE);
        console.log(Platform.OS)
    }

    useEffect(() => {
        fetchNotes();
    }, [])

    const fetchNotes = async () => {
        const response = await noteService.getNotes();
        if (response != undefined) setNotes(response);
    }

    // Add New Note
    const addNote = async () => {
        await someRandomMethodsAsync()
        if (newNote.trim() === '') return;


        console.log(newNote)

        // const response = await noteService.addNote(user.$id, newNote);

        // if (response.error) {
        //   Alert.alert('Error', response.error);
        // } else {
        //   setNotes([...notes, response.data]);
        // }

        setNotes((prevNotes) => [
            ...prevNotes,
            { id: Math.random().toString(), text: newNote }
        ])

        setNewNote('');
        setModalVisible(false);
    };


    // Delete Note
    const deleteNote = async (id: string) => {
        console.log(id)
        Alert.alert('Delete Note', 'Are you sure you want to delete this note?', [
            {
                text: 'Cancel',
                style: 'cancel',
            },
            {
                text: 'Delete',
                style: 'destructive',
                onPress: async () => {
                    Alert.alert(`removing note ${id}`)
                    // const response = await noteService.deleteNote(id);
                    //   if (response.error) {
                    //     Alert.alert('Error', response.error);
                    //   } else {
                    //     setNotes(notes.filter((note) => note.$id !== id));
                    //   }
                },
            },
        ]);
    };

    // Edit Note
    const editNote = async (id: string, newText: string) => {
        if (!newText.trim()) {
            Alert.alert('Error', 'Note text cannot be empty');
            return;
        }


        Alert.alert(`editing note ${id}`)

        // const response = await noteService.updateNote(id, newText);
        // if (response.error) {
        //   Alert.alert('Error', response.error);
        // } else {
        //   setNotes((prevNotes) =>
        //     prevNotes.map((note) =>
        //       note.$id === id ? { ...note, text: response.data.text } : note
        //     )
        //   );
        // }
    };


    return (
        <View style={styles.container}>
            <NoteList notes={notes} onDelete={deleteNote} onEdit={editNote} />


            <TouchableOpacity
                style={styles.addButton}
                onPress={() => setModalVisible(true)}
            >
                <Text style={styles.addButtonText}>+ Add Note</Text>
            </TouchableOpacity>

            {/* Modal */}
            <AddNoteModal
                modalVisible={modalVisible}
                setModalVisible={setModalVisible}
                newNote={newNote}
                setNewNote={setNewNote}
                addNote={addNote}
            />
        </View>
    );
}


const styles = StyleSheet.create({
    container: {
        flex: 1,
        padding: 20,
        backgroundColor: '#fff',
    },
    addButton: {
        position: 'absolute',
        bottom: 20,
        left: 20,
        right: 20,
        backgroundColor: '#007bff',
        padding: 15,
        borderRadius: 8,
        alignItems: 'center',
    },
    addButtonText: {
        color: '#fff',
        fontSize: 18,
        fontWeight: 'bold',
    },
    errorText: {
        color: 'red',
        textAlign: 'center',
        marginBottom: 10,
        fontSize: 16,
    },
    noNotesText: {
        textAlign: 'center',
        fontSize: 18,
        fontWeight: 'bold',
        color: '#555',
        marginTop: 15,
    },
});

export default NoteScreen;