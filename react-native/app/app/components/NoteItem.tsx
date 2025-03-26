import { useState, useRef } from "react";
import { View, Text, StyleSheet, TouchableOpacity, TextInput } from "react-native";

const NoteItem = ({ note, onDelete, onEdit } : any) => {
    const [isEditing, setIsEditing] = useState(false);
    const [editedText, setEditedText] = useState(note.text);
    const inputRef = useRef(null);

    const handleSave = () => {
        if (editedText.trim() === '') return;
        onEdit(note.$id, editedText);
        setIsEditing(false);
    };

    return (
        <View style={styles.noteItem}>
            {isEditing ? (
                <TextInput
                    ref={inputRef}
                    style={styles.input}
                    value={editedText}
                    onChangeText={setEditedText}
                    autoFocus
                    onSubmitEditing={handleSave}
                    returnKeyType='done'
                />
            ) : (
                <Text style={styles.noteText}>{note.text}</Text>
            )}
            <View style={styles.actions}>
                {isEditing ? (
                    <TouchableOpacity
                        onPress={() => {
                            handleSave();
                           // inputRef.current?.blur();
                        }}
                    >
                        <Text style={styles.edit}>💾</Text>
                    </TouchableOpacity>
                ) : (
                    <TouchableOpacity onPress={() => setIsEditing(true)}>
                        <Text style={styles.edit}>✏️</Text>
                    </TouchableOpacity>
                )}

                <TouchableOpacity onPress={() => onDelete(note.$id)}>
                    <Text style={styles.delete}>❌</Text>
                </TouchableOpacity>
            </View>
        </View>
    );
};

const styles = StyleSheet.create({
    noteItem: {
        flexDirection: 'row',
        justifyContent: 'space-between',
        backgroundColor: '#f5f5f5',
        padding: 15,
        borderRadius: 5,
        marginVertical: 5,
    },
    noteText: {
        fontSize: 18,
    },
    delete: {
        fontSize: 18,
        color: 'red',
    },
    actions: {
        flexDirection: 'row',
    },
    edit: {
        fontSize: 18,
        marginRight: 10,
        color: 'blue',
    },
    input: {
      width: '100%',
      padding: 12,
      borderWidth: 1,
      borderColor: '#ddd',
      borderRadius: 8,
      marginBottom: 12,
      backgroundColor: '#fff',
      fontSize: 16,
    },
});

export default NoteItem;
