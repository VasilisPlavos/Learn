import { mockDashboardStats, mockTasks } from "@/data/mockData";
import { Ionicons } from "@expo/vector-icons";
import { SafeAreaView, ScrollView, Text, TouchableOpacity, View } from "react-native";

export default function Index() {
  const stats = mockDashboardStats;
  const recentTasks = mockTasks.filter(task => task.status === 'pending').slice(0, 3);

  const StatCard = ({ title, value, icon, color }: { title: string; value: string | number; icon: string; color: string }) => (
    <View className="bg-white rounded-lg p-4 shadow-sm border border-gray-100 flex-1 mx-1">
      <View className="flex-row items-center justify-between">
        <View>
          <Text className="text-gray-600 text-sm">{title}</Text>
          <Text className="text-2xl font-bold text-gray-900 mt-1">{value}</Text>
        </View>
        <View className={`w-12 h-12 rounded-full items-center justify-center ${color}`}>
          <Ionicons name={icon as any} size={24} color="white" />
        </View>
      </View>
    </View>
  );

  return (
    <SafeAreaView className="flex-1 bg-gray-50">
      <ScrollView className="flex-1">
        {/* Header */}
        <View className="bg-white px-6 py-4 border-b border-gray-200">
          <Text className="text-2xl font-bold text-gray-900">Dashboard</Text>
          <Text className="text-gray-600 mt-1">Welcome back! Here's your CRM overview</Text>
        </View>

        {/* Stats Grid */}
        <View className="px-4 py-6">
          <Text className="text-lg font-semibold text-gray-900 mb-4">Key Metrics</Text>
          
          {/* First Row */}
          <View className="flex-row mb-4">
            <StatCard 
              title="Total Contacts" 
              value={stats.totalContacts} 
              icon="people" 
              color="bg-blue-500" 
            />
            <StatCard 
              title="Active Deals" 
              value={stats.totalDeals} 
              icon="briefcase" 
              color="bg-green-500" 
            />
          </View>

          {/* Second Row */}
          <View className="flex-row mb-4">
            <StatCard 
              title="Revenue" 
              value={`$${(stats.totalRevenue / 1000).toFixed(0)}K`} 
              icon="trending-up" 
              color="bg-purple-500" 
            />
            <StatCard 
              title="Conversion Rate" 
              value={`${stats.conversionRate}%`} 
              icon="analytics" 
              color="bg-orange-500" 
            />
          </View>

          {/* Third Row */}
          <View className="flex-row">
            <StatCard 
              title="Pending Tasks" 
              value={stats.pendingTasks} 
              icon="checkbox" 
              color="bg-red-500" 
            />
            <StatCard 
              title="Monthly Growth" 
              value={`+${stats.monthlyGrowth}%`} 
              icon="arrow-up" 
              color="bg-teal-500" 
            />
          </View>
        </View>

        {/* Recent Tasks */}
        <View className="px-4 pb-6">
          <View className="flex-row items-center justify-between mb-4">
            <Text className="text-lg font-semibold text-gray-900">Recent Tasks</Text>
            <TouchableOpacity>
              <Text className="text-blue-600 font-medium">View All</Text>
            </TouchableOpacity>
          </View>

          <View className="bg-white rounded-lg shadow-sm border border-gray-100">
            {recentTasks.map((task, index) => (
              <View key={task.id} className={`p-4 ${index < recentTasks.length - 1 ? 'border-b border-gray-100' : ''}`}>
                <View className="flex-row items-center justify-between">
                  <View className="flex-1">
                    <Text className="font-medium text-gray-900">{task.title}</Text>
                    <Text className="text-gray-600 text-sm mt-1">{task.description}</Text>
                    <View className="flex-row items-center mt-2">
                      <View className={`px-2 py-1 rounded-full ${
                        task.priority === 'high' ? 'bg-red-100' : 
                        task.priority === 'medium' ? 'bg-yellow-100' : 'bg-green-100'
                      }`}>
                        <Text className={`text-xs font-medium ${
                          task.priority === 'high' ? 'text-red-800' : 
                          task.priority === 'medium' ? 'text-yellow-800' : 'text-green-800'
                        }`}>
                          {task.priority.toUpperCase()}
                        </Text>
                      </View>
                      <Text className="text-gray-500 text-xs ml-2">
                        Due: {task.dueDate.toLocaleDateString()}
                      </Text>
                    </View>
                  </View>
                  <Ionicons 
                    name={task.type === 'call' ? 'call' : task.type === 'email' ? 'mail' : 'document'} 
                    size={20} 
                    color="#64748b" 
                  />
                </View>
              </View>
            ))}
          </View>
        </View>
      </ScrollView>
    </SafeAreaView>
  );
};

