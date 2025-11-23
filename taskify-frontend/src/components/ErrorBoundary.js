import React from 'react';
import { FaExclamationTriangle } from 'react-icons/fa';

/**
 * Error Boundary Component
 * Catches JavaScript errors anywhere in the child component tree
 */
class ErrorBoundary extends React.Component {
  constructor(props) {
    super(props);
    this.state = { 
      hasError: false, 
      error: null,
      errorInfo: null 
    };
  }

  static getDerivedStateFromError(error) {
    return { hasError: true };
  }

  componentDidCatch(error, errorInfo) {
    // Log error to error reporting service
    console.error('Error caught by boundary:', error, errorInfo);
    
    this.setState({
      error,
      errorInfo
    });

    // TODO: Send to error tracking service (e.g., Sentry)
    // logErrorToService(error, errorInfo);
  }

  handleReset = () => {
    this.setState({ 
      hasError: false, 
      error: null,
      errorInfo: null 
    });
  };

  render() {
    if (this.state.hasError) {
      return (
        <div style={{
          display: 'flex',
          flexDirection: 'column',
          alignItems: 'center',
          justifyContent: 'center',
          minHeight: '100vh',
          padding: '20px',
          textAlign: 'center',
          backgroundColor: '#f5f7fa'
        }}>
          <FaExclamationTriangle size={80} color="#dc3545" style={{ marginBottom: '20px' }} />
          <h1 style={{ color: '#333', marginBottom: '10px' }}>Oops! Something went wrong</h1>
          <p style={{ color: '#666', marginBottom: '30px', maxWidth: '500px' }}>
            We're sorry for the inconvenience. The application encountered an unexpected error.
          </p>
          <div style={{ display: 'flex', gap: '15px' }}>
            <button 
              onClick={this.handleReset}
              style={{
                padding: '12px 30px',
                background: 'linear-gradient(135deg, #667eea 0%, #764ba2 100%)',
                color: 'white',
                border: 'none',
                borderRadius: '8px',
                fontSize: '1rem',
                fontWeight: '600',
                cursor: 'pointer'
              }}
            >
              Try Again
            </button>
            <button 
              onClick={() => window.location.href = '/'}
              style={{
                padding: '12px 30px',
                background: '#6c757d',
                color: 'white',
                border: 'none',
                borderRadius: '8px',
                fontSize: '1rem',
                fontWeight: '600',
                cursor: 'pointer'
              }}
            >
              Go Home
            </button>
          </div>
          {process.env.NODE_ENV === 'development' && this.state.error && (
            <details style={{ 
              marginTop: '30px', 
              textAlign: 'left', 
              maxWidth: '800px',
              width: '100%',
              background: 'white',
              padding: '20px',
              borderRadius: '8px',
              boxShadow: '0 2px 8px rgba(0,0,0,0.1)'
            }}>
              <summary style={{ cursor: 'pointer', fontWeight: '600', marginBottom: '10px' }}>
                Error Details (Development Only)
              </summary>
              <pre style={{ 
                overflow: 'auto', 
                padding: '15px', 
                background: '#f8f9fa',
                borderRadius: '4px',
                fontSize: '0.85rem'
              }}>
                {this.state.error.toString()}
                {this.state.errorInfo && this.state.errorInfo.componentStack}
              </pre>
            </details>
          )}
        </div>
      );
    }

    return this.props.children;
  }
}

export default ErrorBoundary;
