import React from 'react';
import { FaEdit, FaTrash, FaCheck, FaClock, FaExclamationCircle } from 'react-icons/fa';
import { format } from 'date-fns';
import { getPriorityLabel, getStatusLabel, TaskStatus } from '../services/taskService';
import './TaskList.css';

const TaskList = ({ tasks, onEdit, onDelete, onToggleStatus }) => {
  if (!tasks || tasks.length === 0) {
    return (
      <div className="empty-state">
        <FaClock size={60} />
        <h3>No tasks found</h3>
        <p>Create your first task to get started!</p>
      </div>
    );
  }

  const getPriorityClass = (priority) => {
    const classes = ['priority-low', 'priority-medium', 'priority-high'];
    return classes[priority] || '';
  };

  const getStatusClass = (status) => {
    return status === TaskStatus.Completed ? 'status-completed' : 'status-pending';
  };

  return (
    <div className="task-list">
      {tasks.map((task) => (
        <div key={task.id} className={`task-card ${getStatusClass(task.status)}`}>
          <div className="task-header">
            <div className="task-title-section">
              <h3 className={task.status === TaskStatus.Completed ? 'completed' : ''}>
                {task.title}
              </h3>
              <div className="task-badges">
                <span className={`badge priority ${getPriorityClass(task.priority)}`}>
                  <FaExclamationCircle /> {getPriorityLabel(task.priority)}
                </span>
                <span className={`badge status ${getStatusClass(task.status)}`}>
                  {task.status === TaskStatus.Completed ? <FaCheck /> : <FaClock />}
                  {getStatusLabel(task.status)}
                </span>
              </div>
            </div>
            <div className="task-actions">
              <button
                className="btn-icon btn-toggle"
                onClick={() => onToggleStatus(task)}
                title={task.status === TaskStatus.Pending ? 'Mark as completed' : 'Mark as pending'}
              >
                <FaCheck />
              </button>
              <button
                className="btn-icon btn-edit"
                onClick={() => onEdit(task)}
                title="Edit task"
              >
                <FaEdit />
              </button>
              <button
                className="btn-icon btn-delete"
                onClick={() => onDelete(task.id)}
                title="Delete task"
              >
                <FaTrash />
              </button>
            </div>
          </div>

          {task.description && (
            <p className="task-description">{task.description}</p>
          )}

          <div className="task-footer">
            <div className="task-dates">
              <span className="task-due-date">
                <strong>Due:</strong> {format(new Date(task.dueDate), 'MMM dd, yyyy')}
              </span>
              <span className="task-created">
                Created: {format(new Date(task.createdAt), 'MMM dd, yyyy')}
              </span>
            </div>
          </div>
        </div>
      ))}
    </div>
  );
};

export default TaskList;
