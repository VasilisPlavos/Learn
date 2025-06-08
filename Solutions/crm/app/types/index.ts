export interface Contact {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  phone?: string;
  company?: string;
  position?: string;
  tags: string[];
  status: 'lead' | 'prospect' | 'customer' | 'inactive';
  createdAt: Date;
  updatedAt: Date;
  lastContactDate?: Date;
  notes?: string;
  avatar?: string;
}

export interface Deal {
  id: string;
  title: string;
  contactId: string;
  value: number;
  currency: string;
  stage: 'prospecting' | 'qualification' | 'proposal' | 'negotiation' | 'closed-won' | 'closed-lost';
  probability: number;
  expectedCloseDate?: Date;
  createdAt: Date;
  updatedAt: Date;
  notes?: string;
}

export interface Campaign {
  id: string;
  name: string;
  type: 'email' | 'sms' | 'social';
  status: 'draft' | 'active' | 'paused' | 'completed';
  subject?: string;
  content: string;
  targetAudience: string[];
  sentCount: number;
  openRate: number;
  clickRate: number;
  createdAt: Date;
  scheduledAt?: Date;
}

export interface Task {
  id: string;
  title: string;
  description?: string;
  type: 'call' | 'email' | 'meeting' | 'follow-up' | 'other';
  priority: 'low' | 'medium' | 'high';
  status: 'pending' | 'completed' | 'overdue';
  dueDate: Date;
  contactId?: string;
  dealId?: string;
  createdAt: Date;
  completedAt?: Date;
}

export interface DashboardStats {
  totalContacts: number;
  totalDeals: number;
  totalRevenue: number;
  activeCampaigns: number;
  pendingTasks: number;
  conversionRate: number;
  monthlyGrowth: number;
}

export interface NavigationProps {
  navigation: any;
  route: any;
}
