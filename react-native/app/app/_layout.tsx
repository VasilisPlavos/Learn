import { Stack } from "expo-router";
const RootLayout = () => {
  return <Stack
    screenOptions={{
      headerStyle: {
        backgroundColor: "#f4511e",
      },
      headerTintColor: "#fff",
      headerTitleStyle: {
        fontSize: 20,
        fontWeight: "bold",
      },
      contentStyle: {
        paddingHorizontal: 10,
        paddingTop: 10,
        backgroundColor: "#fff",
      },
    }}>
    <Stack.Screen name="index" options={{ title: "Booya!" }} />
    <Stack.Screen name="about" options={{ title: "Abouht" }} />
    <Stack.Screen name="notes" options={{ headerTitle: "notes@" }} />
  </Stack>
}

export default RootLayout;