import React from 'react';
import { Link } from 'react-router-dom';
import { authService } from '../services';
import './Dashboard.css';

const Dashboard = () => {
  const user = authService.getCurrentUser();

  return (
    <div className="page-container">
      <div className="dashboard-header">
        <h1>Welcome to PIMS</h1>
        <p>Hello, {user?.fullName || user?.username}!</p>
      </div>

      <div className="dashboard-grid">
        <Link to="/projects" className="dashboard-card">
          <div className="card-icon projects-icon">📊</div>
          <h2>Projects</h2>
          <p>Manage and track all your projects</p>
        </Link>

        <Link to="/users" className="dashboard-card">
          <div className="card-icon users-icon">👥</div>
          <h2>Users</h2>
          <p>View and manage system users</p>
        </Link>

        <Link to="/contracts" className="dashboard-card">
          <div className="card-icon contracts-icon">📝</div>
          <h2>Contracts</h2>
          <p>Track contracts and agreements</p>
        </Link>

        <Link to="/documents" className="dashboard-card">
          <div className="card-icon documents-icon">📁</div>
          <h2>Documents</h2>
          <p>Manage project documents</p>
        </Link>
      </div>

      <div className="info-section">
        <h2>About PIMS</h2>
        <p>
          Project Information Management System (PIMS) is a comprehensive platform for managing
          projects, teams, contracts, and documents. It provides tools for project tracking,
          collaboration, and approval workflows.
        </p>
        <div className="features">
          <div className="feature">
            <h3>✓ Project Management</h3>
            <p>Create, track, and manage projects with detailed information</p>
          </div>
          <div className="feature">
            <h3>✓ Team Collaboration</h3>
            <p>Assign team members and manage project roles</p>
          </div>
          <div className="feature">
            <h3>✓ Contract Tracking</h3>
            <p>Monitor contracts and their approval status</p>
          </div>
          <div className="feature">
            <h3>✓ Document Management</h3>
            <p>Store and version control project documents</p>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Dashboard;
