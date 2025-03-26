import { Note } from "../models/note.interface";

const noteService = {

    // Get Notes
    async getNotes(
        // userId
    ): Promise<Note[] | undefined> {
        //   if (!userId) {
        //     console.error('Error: Missing userId in getNotes()');
        //     return {
        //       data: [],
        //       error: 'User ID is missing',
        //     };
        //   }

        try {
            // const response = await databaseService.listDocuments(dbId, colId, [
            //   Query.equal('user_id', userId),
            // ]);

            const response = new Array<Note>({ id: "1", text: "Note one" },
                { id: "2", text: "Note two" },
                { id: "3", text: "Note three" },
                { id: "4", text: "Note four" },
                { id: "5", text: "Note five" },
                { id: "6", text: "Note six" },);

            return response;
        } catch (error) {

           // console.log('Error fetching notes:', error.message);
           console.log('Error fetching notes:', error);
           
           throw new Error('Error fetching notes');
            // return { data: [], error: error.message };
        }
    },
    // // Add New Note
    // async addNote(user_id, text) {
    //   if (!text) {
    //     return { error: 'Note text cannot be empty' };
    //   }

    //   const data = {
    //     text: text,
    //     createdAt: new Date().toISOString(),
    //     user_id: user_id,
    //   };

    //   const response = await databaseService.createDocument(
    //     dbId,
    //     colId,
    //     data,
    //     ID.unique()
    //   );

    //   if (response?.error) {
    //     return { error: response.error };
    //   }

    //   return { data: response };
    // },
    // // Update Note
    // async updateNote(id, text) {
    //   const response = await databaseService.updateDocument(dbId, colId, id, {
    //     text,
    //   });

    //   if (response?.error) {
    //     return { error: response.error };
    //   }

    //   return { data: response };
    // },
    // // Delete Note
    // async deleteNote(id) {
    //   const response = await databaseService.deleteDocument(dbId, colId, id);
    //   if (response?.error) {
    //     return { error: response.error };
    //   }

    //   return { success: true };
    // },
};

export default noteService;