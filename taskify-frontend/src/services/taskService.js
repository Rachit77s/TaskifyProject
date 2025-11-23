import axios from 'axios';

const API_BASE_URL = 'http://localhost:5226/api';

const api = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

// Add request interceptor to include token
api.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem('token');
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

// Add response interceptor to handle 401 errors
api.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response?.status === 401) {
      // Token expired or invalid - clear auth data
      localStorage.removeItem('token');
      localStorage.removeItem('user');
      // Note: Don't auto-redirect to avoid infinite loops
      // Let the component handle the error state instead
      console.warn('Authentication token is invalid or expired');
    }
    return Promise.reject(error);
  }
);

/**
 * Task Priority Enum
 */
export const TaskPriority = {
  Low: 0,
  Medium: 1,
  High: 2,
};

/**
 * Task Status Enum
 */
export const TaskStatus = {
  Pending: 0,
  Completed: 1,
};

/**
 * Get priority label
 */
export const getPriorityLabel = (priority) => {
  const labels = ['Low', 'Medium', 'High'];
  return labels[priority] || 'Unknown';
};

/**
 * Get status label
 */
export const getStatusLabel = (status) => {
  const labels = ['Pending', 'Completed'];
  return labels[status] || 'Unknown';
};

/**
 * Task Service - API calls for task management
 */
const taskService = {
  /**
   * Get all tasks for the authenticated user
   */
  getAllTasks: async () => {
    const response = await api.get('/tasks');
    return response.data;
  },

  /**
   * Get task by ID (only if it belongs to authenticated user)
   */
  getTaskById: async (id) => {
    const response = await api.get(`/tasks/${id}`);
    return response.data;
  },

  /**
   * Get filtered tasks with pagination (for authenticated user)
   */
  getFilteredTasks: async (filters) => {
    const params = new URLSearchParams();
    
    if (filters.status !== null && filters.status !== undefined) {
      params.append('status', filters.status);
    }
    if (filters.priority !== null && filters.priority !== undefined) {
      params.append('priority', filters.priority);
    }
    if (filters.pageNumber) {
      params.append('pageNumber', filters.pageNumber);
    }
    if (filters.pageSize) {
      params.append('pageSize', filters.pageSize);
    }

    const response = await api.get(`/tasks/filter?${params.toString()}`);
    return response.data;
  },

  /**
   * Create a new task (for authenticated user)
   */
  createTask: async (taskData) => {
    const response = await api.post('/tasks', taskData);
    return response.data;
  },

  /**
   * Update a task (only if it belongs to authenticated user)
   */
  updateTask: async (id, taskData) => {
    const response = await api.put(`/tasks/${id}`, taskData);
    return response.data;
  },

  /**
   * Delete a task (only if it belongs to authenticated user)
   */
  deleteTask: async (id) => {
    const response = await api.delete(`/tasks/${id}`);
    return response.data;
  },
};

export default taskService;
