import React, { useState, useEffect } from 'react';
import { documentService } from '../services';
import './Documents.css';

const Documents = () => {
  const [documents, setDocuments] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');

  useEffect(() => {
    loadDocuments();
  }, []);

  const loadDocuments = async () => {
    try {
      const data = await documentService.getAll();
      setDocuments(data);
      setError('');
    } catch (err) {
      setError('Failed to load documents');
    } finally {
      setLoading(false);
    }
  };

  const formatFileSize = (bytes) => {
    if (bytes === 0) return '0 Bytes';
    const k = 1024;
    const sizes = ['Bytes', 'KB', 'MB', 'GB'];
    const i = Math.floor(Math.log(bytes) / Math.log(k));
    return Math.round(bytes / Math.pow(k, i) * 100) / 100 + ' ' + sizes[i];
  };

  if (loading) return <div className="loading">Loading documents...</div>;

  return (
    <div className="page-container">
      <div className="page-header">
        <h1>Documents</h1>
      </div>

      {error && <div className="error-message">{error}</div>}

      <div className="table-card">
        <table className="data-table">
          <thead>
            <tr>
              <th>File Name</th>
              <th>Description</th>
              <th>Size</th>
              <th>Version</th>
              <th>Uploaded By</th>
              <th>Uploaded Date</th>
              <th>Status</th>
            </tr>
          </thead>
          <tbody>
            {documents.map((doc) => (
              <tr key={doc.documentId}>
                <td className="file-name">{doc.fileName}</td>
                <td>{doc.description}</td>
                <td>{formatFileSize(doc.fileSize)}</td>
                <td>v{doc.version}</td>
                <td>{doc.uploadedBy}</td>
                <td>{new Date(doc.uploadedDate).toLocaleDateString()}</td>
                <td>
                  <span className={`status-badge ${doc.status?.toLowerCase()}`}>
                    {doc.status}
                  </span>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default Documents;
