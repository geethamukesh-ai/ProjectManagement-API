import React, { useState, useEffect } from 'react';
import { projectService } from '../services';
import { Link } from 'react-router-dom';
import './Projects.css';

const Projects = () => {
  const [projects, setProjects] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const [showForm, setShowForm] = useState(false);
  const [formData, setFormData] = useState({
    projectCode: '',
    projectName: '',
    clientName: '',
    status: 'Planning',
    description: '',
    startDate: '',
    endDate: '',
    budget: 0
  });

  useEffect(() => {
    loadProjects();
  }, []);

  const loadProjects = async () => {
    try {
      const data = await projectService.getAll();
      setProjects(data);
      setError('');
    } catch (err) {
      setError('Failed to load projects');
    } finally {
      setLoading(false);
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await projectService.create(formData);
      setShowForm(false);
      setFormData({
        projectCode: '',
        projectName: '',
        clientName: '',
        status: 'Planning',
        description: '',
        startDate: '',
        endDate: '',
        budget: 0
      });
      loadProjects();
    } catch (err) {
      setError('Failed to create project');
    }
  };

  const handleDelete = async (id) => {
    if (window.confirm('Are you sure you want to delete this project?')) {
      try {
        await projectService.delete(id);
        loadProjects();
      } catch (err) {
        setError('Failed to delete project');
      }
    }
  };

  if (loading) return <div className="loading">Loading projects...</div>;

  return (
    <div className="page-container">
      <div className="page-header">
        <h1>Projects</h1>
        <button className="btn-primary" onClick={() => setShowForm(!showForm)}>
          {showForm ? 'Cancel' : 'New Project'}
        </button>
      </div>

      {error && <div className="error-message">{error}</div>}

      {showForm && (
        <div className="form-card">
          <h2>Create New Project</h2>
          <form onSubmit={handleSubmit}>
            <div className="form-row">
              <div className="form-group">
                <label>Project Code</label>
                <input
                  type="text"
                  value={formData.projectCode}
                  onChange={(e) => setFormData({ ...formData, projectCode: e.target.value })}
                  required
                />
              </div>
              <div className="form-group">
                <label>Project Name</label>
                <input
                  type="text"
                  value={formData.projectName}
                  onChange={(e) => setFormData({ ...formData, projectName: e.target.value })}
                  required
                />
              </div>
            </div>
            <div className="form-row">
              <div className="form-group">
                <label>Client Name</label>
                <input
                  type="text"
                  value={formData.clientName}
                  onChange={(e) => setFormData({ ...formData, clientName: e.target.value })}
                />
              </div>
              <div className="form-group">
                <label>Status</label>
                <select
                  value={formData.status}
                  onChange={(e) => setFormData({ ...formData, status: e.target.value })}
                >
                  <option>Planning</option>
                  <option>In Progress</option>
                  <option>On Hold</option>
                  <option>Completed</option>
                  <option>Cancelled</option>
                </select>
              </div>
            </div>
            <div className="form-group">
              <label>Description</label>
              <textarea
                value={formData.description}
                onChange={(e) => setFormData({ ...formData, description: e.target.value })}
                rows="3"
              />
            </div>
            <div className="form-row">
              <div className="form-group">
                <label>Start Date</label>
                <input
                  type="date"
                  value={formData.startDate}
                  onChange={(e) => setFormData({ ...formData, startDate: e.target.value })}
                  required
                />
              </div>
              <div className="form-group">
                <label>End Date</label>
                <input
                  type="date"
                  value={formData.endDate}
                  onChange={(e) => setFormData({ ...formData, endDate: e.target.value })}
                  required
                />
              </div>
              <div className="form-group">
                <label>Budget</label>
                <input
                  type="number"
                  value={formData.budget}
                  onChange={(e) => setFormData({ ...formData, budget: parseFloat(e.target.value) })}
                  required
                />
              </div>
            </div>
            <button type="submit" className="btn-primary">Create Project</button>
          </form>
        </div>
      )}

      <div className="projects-grid">
        {projects.map((project) => (
          <div key={project.projectId} className="project-card">
            <div className="project-header">
              <h3>{project.projectName}</h3>
              <span className={`status-badge ${project.status?.toLowerCase().replace(' ', '-')}`}>
                {project.status}
              </span>
            </div>
            <p className="project-code">{project.projectCode}</p>
            <p className="project-client">Client: {project.clientName}</p>
            <p className="project-description">{project.description}</p>
            <div className="project-footer">
              <span className="project-budget">Budget: ${project.budget?.toLocaleString()}</span>
              <div className="project-actions">
                <Link to={`/projects/${project.projectId}`} className="btn-view">View</Link>
                <button onClick={() => handleDelete(project.projectId)} className="btn-delete">Delete</button>
              </div>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};

export default Projects;
