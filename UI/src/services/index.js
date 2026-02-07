import api from './api';

export const authService = {
  async login(credentials) {
    const response = await api.post('/auth/login', credentials);
    if (response.data.token) {
      localStorage.setItem('token', response.data.token);
      localStorage.setItem('user', JSON.stringify({
        username: response.data.username,
        fullName: response.data.fullName,
        role: response.data.role
      }));
    }
    return response.data;
  },

  async register(userData) {
    const response = await api.post('/auth/register', userData);
    if (response.data.token) {
      localStorage.setItem('token', response.data.token);
      localStorage.setItem('user', JSON.stringify({
        username: response.data.username,
        fullName: response.data.fullName,
        role: response.data.role
      }));
    }
    return response.data;
  },

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
  },

  getCurrentUser() {
    const user = localStorage.getItem('user');
    return user ? JSON.parse(user) : null;
  },

  isAuthenticated() {
    return !!localStorage.getItem('token');
  }
};

export const projectService = {
  async getAll() {
    const response = await api.get('/projects');
    return response.data;
  },

  async getById(id) {
    const response = await api.get(`/projects/${id}`);
    return response.data;
  },

  async create(project) {
    const response = await api.post('/projects', project);
    return response.data;
  },

  async update(id, project) {
    const response = await api.put(`/projects/${id}`, project);
    return response.data;
  },

  async delete(id) {
    await api.delete(`/projects/${id}`);
  }
};

export const userService = {
  async getAll() {
    const response = await api.get('/users');
    return response.data;
  },

  async getById(id) {
    const response = await api.get(`/users/${id}`);
    return response.data;
  }
};

export const contractService = {
  async getAll() {
    const response = await api.get('/contracts');
    return response.data;
  },

  async getByProject(projectId) {
    const response = await api.get(`/contracts/project/${projectId}`);
    return response.data;
  },

  async create(contract) {
    const response = await api.post('/contracts', contract);
    return response.data;
  },

  async update(id, contract) {
    const response = await api.put(`/contracts/${id}`, contract);
    return response.data;
  },

  async delete(id) {
    await api.delete(`/contracts/${id}`);
  }
};

export const documentService = {
  async getAll() {
    const response = await api.get('/documents');
    return response.data;
  },

  async getByProject(projectId) {
    const response = await api.get(`/documents/project/${projectId}`);
    return response.data;
  },

  async create(document) {
    const response = await api.post('/documents', document);
    return response.data;
  },

  async update(id, document) {
    const response = await api.put(`/documents/${id}`, document);
    return response.data;
  },

  async delete(id) {
    await api.delete(`/documents/${id}`);
  }
};

export const approvalService = {
  async getByDocument(documentId) {
    const response = await api.get(`/approvals/document/${documentId}`);
    return response.data;
  },

  async create(approval) {
    const response = await api.post('/approvals', approval);
    return response.data;
  },

  async update(id, approval) {
    const response = await api.put(`/approvals/${id}`, approval);
    return response.data;
  }
};
