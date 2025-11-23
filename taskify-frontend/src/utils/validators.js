/**
 * Validation Utilities
 * Reusable validation functions for form fields and data
 */

/**
 * Validate email format
 */
export const isValidEmail = (email) => {
  const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  return emailRegex.test(email);
};

/**
 * Validate password strength
 * At least 8 characters, 1 uppercase, 1 lowercase, 1 number
 */
export const isValidPassword = (password) => {
  const minLength = password.length >= 8;
  const hasUpperCase = /[A-Z]/.test(password);
  const hasLowerCase = /[a-z]/.test(password);
  const hasNumber = /\d/.test(password);
  
  return minLength && hasUpperCase && hasLowerCase && hasNumber;
};

/**
 * Validate string length
 */
export const isValidLength = (str, min, max) => {
  const length = str?.trim().length || 0;
  return length >= min && length <= max;
};

/**
 * Validate required field
 */
export const isRequired = (value) => {
  if (typeof value === 'string') {
    return value.trim().length > 0;
  }
  return value !== null && value !== undefined;
};

/**
 * Validate date is in the future
 */
export const isFutureDate = (dateString) => {
  const date = new Date(dateString);
  const today = new Date();
  today.setHours(0, 0, 0, 0);
  return date >= today;
};

/**
 * Sanitize input to prevent XSS
 */
export const sanitizeInput = (input) => {
  if (typeof input !== 'string') return input;
  
  return input
    .replace(/&/g, '&amp;')
    .replace(/</g, '&lt;')
    .replace(/>/g, '&gt;')
    .replace(/"/g, '&quot;')
    .replace(/'/g, '&#x27;')
    .replace(/\//g, '&#x2F;');
};

/**
 * Validate task form data
 */
export const validateTaskForm = (data) => {
  const errors = {};

  if (!isRequired(data.title)) {
    errors.title = 'Title is required';
  } else if (!isValidLength(data.title, 1, 200)) {
    errors.title = 'Title must be between 1 and 200 characters';
  }

  if (data.description && !isValidLength(data.description, 0, 1000)) {
    errors.description = 'Description must be 1000 characters or less';
  }

  if (!isRequired(data.dueDate)) {
    errors.dueDate = 'Due date is required';
  } else if (!isFutureDate(data.dueDate)) {
    errors.dueDate = 'Due date must be today or in the future';
  }

  return {
    isValid: Object.keys(errors).length === 0,
    errors
  };
};
