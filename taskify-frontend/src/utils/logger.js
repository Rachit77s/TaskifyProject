/**
 * Logger Utility
 * Centralized logging with different log levels
 */

const LOG_LEVELS = {
  DEBUG: 0,
  INFO: 1,
  WARN: 2,
  ERROR: 3,
};

const CURRENT_LEVEL = process.env.NODE_ENV === 'production' 
  ? LOG_LEVELS.WARN 
  : LOG_LEVELS.DEBUG;

/**
 * Log debug information
 */
export const logDebug = (message, data = null) => {
  if (CURRENT_LEVEL <= LOG_LEVELS.DEBUG) {
    console.log(`[DEBUG] ${message}`, data || '');
  }
};

/**
 * Log informational messages
 */
export const logInfo = (message, data = null) => {
  if (CURRENT_LEVEL <= LOG_LEVELS.INFO) {
    console.info(`[INFO] ${message}`, data || '');
  }
};

/**
 * Log warnings
 */
export const logWarning = (message, data = null) => {
  if (CURRENT_LEVEL <= LOG_LEVELS.WARN) {
    console.warn(`[WARN] ${message}`, data || '');
  }
};

/**
 * Log errors
 */
export const logError = (message, error = null) => {
  if (CURRENT_LEVEL <= LOG_LEVELS.ERROR) {
    console.error(`[ERROR] ${message}`, error || '');
    
    // TODO: Send to error tracking service in production
    // if (process.env.NODE_ENV === 'production') {
    //   sendToErrorTracking({ message, error });
    // }
  }
};

/**
 * Log API errors with additional context
 */
export const logApiError = (endpoint, error) => {
  const errorData = {
    endpoint,
    status: error.response?.status,
    message: error.response?.data?.message || error.message,
    timestamp: new Date().toISOString(),
  };
  
  logError('API Error', errorData);
};
