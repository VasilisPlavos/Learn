import React from "react";
import { View, FlatList } from "react-native";
import NoteItem from "./NoteItem";
import { Note } from "../models/note.interface";

const NoteList = ({ notes, onDelete, onEdit } : any) => {
  return (
    <View>
      <FlatList
        data={notes}
        keyExtractor={(item:Note) => item.id}
        renderItem={({ item }) => (
          <NoteItem note={item} onDelete={onDelete} onEdit={onEdit} />
        )}
      />
    </View>
  );
}

export default NoteList;