import React from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { authService } from '../services';
import './Navbar.css';

const Navbar = () => {
  const navigate = useNavigate();
  const user = authService.getCurrentUser();

  const handleLogout = () => {
    authService.logout();
    navigate('/login');
  };

  if (!authService.isAuthenticated()) {
    return null;
  }

  return (
    <nav className="navbar">
      <div className="navbar-brand">
        <Link to="/">PIMS</Link>
      </div>
      <div className="navbar-menu">
        <Link to="/projects">Projects</Link>
        <Link to="/users">Users</Link>
        <Link to="/contracts">Contracts</Link>
        <Link to="/documents">Documents</Link>
      </div>
      <div className="navbar-user">
        <span>{user?.fullName || user?.username}</span>
        <span className="user-role">({user?.role})</span>
        <button onClick={handleLogout} className="logout-btn">Logout</button>
      </div>
    </nav>
  );
};

export default Navbar;
