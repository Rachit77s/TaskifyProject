import React, { useState, useEffect } from 'react';
import './App.css';
import { useAuth } from './context/AuthContext';
import taskService, { TaskPriority, TaskStatus } from './services/taskService';
import TaskList from './components/TaskList';
import TaskForm from './components/TaskForm';
import FilterBar from './components/FilterBar';
import { FaTasks, FaExclamationCircle, FaCheckCircle, FaUser, FaSignOutAlt } from 'react-icons/fa';
import { useNavigate } from 'react-router-dom';

function App() {
  const [tasks, setTasks] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [showForm, setShowForm] = useState(false);
  const [editingTask, setEditingTask] = useState(null);
  const [filters, setFilters] = useState({
    status: null,
    priority: null,
    pageNumber: 1,
    pageSize: 10,
  });
  const [pagination, setPagination] = useState({
    totalCount: 0,
    totalPages: 0,
  });

  const { user, logout } = useAuth();
  const navigate = useNavigate();

  // Fetch tasks
  useEffect(() => {
    fetchTasks();
  }, [filters]);

  const fetchTasks = async () => {
    try {
      setLoading(true);
      setError(null);

      let response;
      
      // Check if filters are applied
      if (filters.status !== null || filters.priority !== null) {
        response = await taskService.getFilteredTasks(filters);
      } else {
        response = await taskService.getAllTasks();
      }

      if (response.success) {
        if (filters.status !== null || filters.priority !== null) {
          setTasks(response.data.tasks);
          setPagination({
            totalCount: response.data.totalCount,
            totalPages: response.data.totalPages,
          });
        } else {
          setTasks(response.data);
          setPagination({
            totalCount: response.data.length,
            totalPages: 1,
          });
        }
      }
    } catch (err) {
      setError(err.response?.data?.message || 'Failed to fetch tasks. Please try again.');
      console.error('Error fetching tasks:', err);
    } finally {
      setLoading(false);
    }
  };

  const handleCreateTask = async (taskData) => {
    try {
      const response = await taskService.createTask(taskData);
      if (response.success) {
        setShowForm(false);
        fetchTasks();
      }
    } catch (err) {
      throw new Error(err.response?.data?.message || 'Failed to create task');
    }
  };

  const handleUpdateTask = async (id, taskData) => {
    try {
      const response = await taskService.updateTask(id, taskData);
      if (response.success) {
        setShowForm(false);
        setEditingTask(null);
        fetchTasks();
      }
    } catch (err) {
      throw new Error(err.response?.data?.message || 'Failed to update task');
    }
  };

  const handleDeleteTask = async (id) => {
    if (window.confirm('Are you sure you want to delete this task?')) {
      try {
        const response = await taskService.deleteTask(id);
        if (response.success) {
          fetchTasks();
        }
      } catch (err) {
        alert(err.response?.data?.message || 'Failed to delete task');
      }
    }
  };

  const handleEditClick = (task) => {
    setEditingTask(task);
    setShowForm(true);
  };

  const handleToggleStatus = async (task) => {
    const newStatus = task.status === TaskStatus.Pending ? TaskStatus.Completed : TaskStatus.Pending;
    try {
      await taskService.updateTask(task.id, { status: newStatus });
      fetchTasks();
    } catch (err) {
      alert('Failed to update task status');
    }
  };

  const handleFilterChange = (newFilters) => {
    setFilters({ ...newFilters, pageNumber: 1 });
  };

  const handleClearFilters = () => {
    setFilters({
      status: null,
      priority: null,
      pageNumber: 1,
      pageSize: 10,
    });
  };

  const handlePageChange = (newPage) => {
    setFilters({ ...filters, pageNumber: newPage });
  };

  const handleCloseForm = () => {
    setShowForm(false);
    setEditingTask(null);
  };

  const handleLogout = () => {
    if (window.confirm('Are you sure you want to logout?')) {
      logout();
      navigate('/login');
    }
  };

  // Calculate stats
  const stats = {
    total: tasks.length,
    pending: tasks.filter(t => t.status === TaskStatus.Pending).length,
    completed: tasks.filter(t => t.status === TaskStatus.Completed).length,
    highPriority: tasks.filter(t => t.priority === TaskPriority.High).length,
  };

  return (
    <div className="App">
      <div className="app-header">
        <div>
          <h1><FaTasks /> Taskify</h1>
          <p>Manage your tasks efficiently and stay organized</p>
        </div>
        <div className="user-info">
          <div className="user-details">
            <FaUser />
            <div>
              <strong>{user?.firstName || user?.username}</strong>
              <span className="user-role">{user?.role}</span>
            </div>
          </div>
          <button className="btn-logout" onClick={handleLogout} title="Logout">
            <FaSignOutAlt /> Logout
          </button>
        </div>
      </div>

      <div className="app-content">
        {/* Filter Bar */}
        <FilterBar
          filters={filters}
          onFilterChange={handleFilterChange}
          onClearFilters={handleClearFilters}
        />

        {/* Action Bar */}
        <div className="action-bar">
          <h2><FaTasks /> My Tasks</h2>
          <button className="btn-add-task" onClick={() => setShowForm(true)}>
            <span>+</span> Add New Task
          </button>
        </div>

        {/* Stats Section */}
        <div className="stats-section">
          <div className="stat-card">
            <h4>Total Tasks</h4>
            <p>{stats.total}</p>
          </div>
          <div className="stat-card">
            <h4><FaExclamationCircle /> Pending</h4>
            <p>{stats.pending}</p>
          </div>
          <div className="stat-card">
            <h4><FaCheckCircle /> Completed</h4>
            <p>{stats.completed}</p>
          </div>
          <div className="stat-card">
            <h4>High Priority</h4>
            <p>{stats.highPriority}</p>
          </div>
        </div>

        {/* Task List */}
        {loading ? (
          <div className="loading-container">
            <div className="loading-spinner"></div>
            <p>Loading tasks...</p>
          </div>
        ) : error ? (
          <div className="error-container">
            <FaExclamationCircle />
            <h3>Error</h3>
            <p>{error}</p>
            <button className="btn-retry" onClick={fetchTasks}>Retry</button>
          </div>
        ) : (
          <>
            <TaskList
              tasks={tasks}
              onEdit={handleEditClick}
              onDelete={handleDeleteTask}
              onToggleStatus={handleToggleStatus}
            />

            {/* Pagination */}
            {pagination.totalPages > 1 && (
              <div className="pagination">
                <button
                  onClick={() => handlePageChange(filters.pageNumber - 1)}
                  disabled={filters.pageNumber === 1}
                >
                  Previous
                </button>
                <span>
                  Page {filters.pageNumber} of {pagination.totalPages}
                </span>
                <button
                  onClick={() => handlePageChange(filters.pageNumber + 1)}
                  disabled={filters.pageNumber === pagination.totalPages}
                >
                  Next
                </button>
              </div>
            )}
          </>
        )}
      </div>

      {/* Task Form Modal */}
      {showForm && (
        <TaskForm
          task={editingTask}
          onSubmit={editingTask ? handleUpdateTask : handleCreateTask}
          onClose={handleCloseForm}
        />
      )}
    </div>
  );
}

export default App;
