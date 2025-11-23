import React from 'react';
import { FaFilter } from 'react-icons/fa';
import './FilterBar.css';

const FilterBar = ({ filters, onFilterChange, onClearFilters }) => {
  const handleChange = (e) => {
    const { name, value } = e.target;
    const newValue = value === '' ? null : parseInt(value);
    onFilterChange({ ...filters, [name]: newValue });
  };

  const hasActiveFilters = filters.status !== null || filters.priority !== null;

  return (
    <div className="filter-section">
      <h3>
        <FaFilter /> Filter Tasks
      </h3>
      <div className="filter-controls">
        <div className="filter-group">
          <label htmlFor="status">Status</label>
          <select
            id="status"
            name="status"
            value={filters.status === null ? '' : filters.status}
            onChange={handleChange}
          >
            <option value="">All Status</option>
            <option value={0}>Pending</option>
            <option value={1}>Completed</option>
          </select>
        </div>

        <div className="filter-group">
          <label htmlFor="priority">Priority</label>
          <select
            id="priority"
            name="priority"
            value={filters.priority === null ? '' : filters.priority}
            onChange={handleChange}
          >
            <option value="">All Priorities</option>
            <option value={0}>Low</option>
            <option value={1}>Medium</option>
            <option value={2}>High</option>
          </select>
        </div>

        <div className="filter-group">
          <label htmlFor="pageSize">Items per Page</label>
          <select
            id="pageSize"
            name="pageSize"
            value={filters.pageSize}
            onChange={(e) => onFilterChange({ ...filters, pageSize: parseInt(e.target.value), pageNumber: 1 })}
          >
            <option value={5}>5</option>
            <option value={10}>10</option>
            <option value={20}>20</option>
            <option value={50}>50</option>
          </select>
        </div>

        {hasActiveFilters && (
          <button className="btn-clear-filters" onClick={onClearFilters}>
            Clear Filters
          </button>
        )}
      </div>
    </div>
  );
};

export default FilterBar;
