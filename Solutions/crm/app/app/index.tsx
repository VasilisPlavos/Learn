import { Text, View } from "react-native";

export default function Index() {
  return (
    <View
      style={{
        flex: 1,
        justifyContent: "center",
        alignItems: "center",
      }}
    >
      <Text className="font-extrabold">Edit app/index.tsx to edit this screen.</Text>
        <Text className='text-yellow-700'>Tailwind test</Text>
    </View>
  );
}
