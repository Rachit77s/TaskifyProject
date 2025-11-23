import React, { useState, useEffect } from 'react';
import { FaTimes } from 'react-icons/fa';
import { TaskPriority } from '../services/taskService';
import './TaskForm.css';

const TaskForm = ({ task, onSubmit, onClose }) => {
  const [formData, setFormData] = useState({
    title: '',
    description: '',
    dueDate: '',
    priority: TaskPriority.Medium,
  });
  const [errors, setErrors] = useState({});
  const [submitting, setSubmitting] = useState(false);

  useEffect(() => {
    if (task) {
      setFormData({
        title: task.title,
        description: task.description || '',
        dueDate: new Date(task.dueDate).toISOString().split('T')[0],
        priority: task.priority,
        status: task.status,
      });
    }
  }, [task]);

  const validate = () => {
    const newErrors = {};

    if (!formData.title.trim()) {
      newErrors.title = 'Title is required';
    } else if (formData.title.length > 200) {
      newErrors.title = 'Title must be 200 characters or less';
    }

    if (formData.description && formData.description.length > 1000) {
      newErrors.description = 'Description must be 1000 characters or less';
    }

    if (!formData.dueDate) {
      newErrors.dueDate = 'Due date is required';
    }

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: name === 'priority' ? parseInt(value) : value,
    });
    // Clear error for this field
    if (errors[name]) {
      setErrors({ ...errors, [name]: '' });
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!validate()) {
      return;
    }

    setSubmitting(true);

    try {
      const submitData = {
        title: formData.title.trim(),
        description: formData.description.trim() || null,
        dueDate: new Date(formData.dueDate).toISOString(),
        priority: formData.priority,
      };

      if (task) {
        // Include status when editing
        submitData.status = formData.status;
        await onSubmit(task.id, submitData);
      } else {
        await onSubmit(submitData);
      }
    } catch (err) {
      alert(err.message || 'Failed to save task');
    } finally {
      setSubmitting(false);
    }
  };

  return (
    <div className="modal-overlay" onClick={onClose}>
      <div className="modal-content" onClick={(e) => e.stopPropagation()}>
        <div className="modal-header">
          <h2>{task ? 'Edit Task' : 'Create New Task'}</h2>
          <button className="btn-close" onClick={onClose}>
            <FaTimes />
          </button>
        </div>

        <form onSubmit={handleSubmit} className="task-form">
          <div className="form-group">
            <label htmlFor="title">
              Title <span className="required">*</span>
            </label>
            <input
              type="text"
              id="title"
              name="title"
              value={formData.title}
              onChange={handleChange}
              className={errors.title ? 'error' : ''}
              placeholder="Enter task title"
              maxLength={200}
            />
            {errors.title && <span className="error-message">{errors.title}</span>}
            <small className="char-count">{formData.title.length}/200</small>
          </div>

          <div className="form-group">
            <label htmlFor="description">Description</label>
            <textarea
              id="description"
              name="description"
              value={formData.description}
              onChange={handleChange}
              className={errors.description ? 'error' : ''}
              placeholder="Enter task description (optional)"
              rows={4}
              maxLength={1000}
            />
            {errors.description && <span className="error-message">{errors.description}</span>}
            <small className="char-count">{formData.description.length}/1000</small>
          </div>

          <div className="form-row">
            <div className="form-group">
              <label htmlFor="dueDate">
                Due Date <span className="required">*</span>
              </label>
              <input
                type="date"
                id="dueDate"
                name="dueDate"
                value={formData.dueDate}
                onChange={handleChange}
                className={errors.dueDate ? 'error' : ''}
                min={new Date().toISOString().split('T')[0]}
              />
              {errors.dueDate && <span className="error-message">{errors.dueDate}</span>}
            </div>

            <div className="form-group">
              <label htmlFor="priority">
                Priority <span className="required">*</span>
              </label>
              <select
                id="priority"
                name="priority"
                value={formData.priority}
                onChange={handleChange}
              >
                <option value={TaskPriority.Low}>Low</option>
                <option value={TaskPriority.Medium}>Medium</option>
                <option value={TaskPriority.High}>High</option>
              </select>
            </div>
          </div>

          {task && (
            <div className="form-group">
              <label htmlFor="status">Status</label>
              <select
                id="status"
                name="status"
                value={formData.status}
                onChange={handleChange}
              >
                <option value={0}>Pending</option>
                <option value={1}>Completed</option>
              </select>
            </div>
          )}

          <div className="form-actions">
            <button type="button" className="btn-cancel" onClick={onClose}>
              Cancel
            </button>
            <button type="submit" className="btn-submit" disabled={submitting}>
              {submitting ? 'Saving...' : task ? 'Update Task' : 'Create Task'}
            </button>
          </div>
        </form>
      </div>
    </div>
  );
};

export default TaskForm;
