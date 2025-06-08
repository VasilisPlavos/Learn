import './global.css'
import { Stack, Tabs } from "expo-router";
import { Ionicons } from "@expo/vector-icons";

export default function RootLayout() {
  return (
    <Tabs
      screenOptions={({ route }) => ({
        tabBarIcon: ({ focused, color, size }) => {
          let iconName: keyof typeof Ionicons.glyphMap;

          switch (route.name) {
            case 'index':
              iconName = focused ? 'analytics' : 'analytics-outline';
              break;
            case 'contacts':
              iconName = focused ? 'people' : 'people-outline';
              break;
            case 'deals':
              iconName = focused ? 'briefcase' : 'briefcase-outline';
              break;
            case 'campaigns':
              iconName = focused ? 'mail' : 'mail-outline';
              break;
            case 'tasks':
              iconName = focused ? 'checkbox' : 'checkbox-outline';
              break;
            default:
              iconName = 'ellipse';
          }

          return <Ionicons name={iconName} size={size} color={color} />;
        },
        tabBarActiveTintColor: '#3b82f6',
        tabBarInactiveTintColor: '#64748b',
        headerShown: false,
      })}
    >
      <Tabs.Screen name="dashboard" options={{ title: 'Dashboard' }} />
      {/* <Tabs.Screen name="contacts" options={{ title: 'Contacts' }} />
      <Tabs.Screen name="deals" options={{ title: 'Deals' }} />
      <Tabs.Screen name="campaigns" options={{ title: 'Campaigns' }} />
      <Tabs.Screen name="tasks" options={{ title: 'Tasks' }} /> */}
    </Tabs>
  );
}