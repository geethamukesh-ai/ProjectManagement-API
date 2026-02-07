import React, { useState, useEffect } from 'react';
import { contractService } from '../services';
import './Contracts.css';

const Contracts = () => {
  const [contracts, setContracts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');

  useEffect(() => {
    loadContracts();
  }, []);

  const loadContracts = async () => {
    try {
      const data = await contractService.getAll();
      setContracts(data);
      setError('');
    } catch (err) {
      setError('Failed to load contracts');
    } finally {
      setLoading(false);
    }
  };

  if (loading) return <div className="loading">Loading contracts...</div>;

  return (
    <div className="page-container">
      <div className="page-header">
        <h1>Contracts</h1>
      </div>

      {error && <div className="error-message">{error}</div>}

      <div className="table-card">
        <table className="data-table">
          <thead>
            <tr>
              <th>Contract #</th>
              <th>Type</th>
              <th>Vendor</th>
              <th>Amount</th>
              <th>Start Date</th>
              <th>End Date</th>
              <th>Status</th>
              <th>Approval Status</th>
            </tr>
          </thead>
          <tbody>
            {contracts.map((contract) => (
              <tr key={contract.contractId}>
                <td>{contract.contractNumber}</td>
                <td>{contract.contractType}</td>
                <td>{contract.vendor}</td>
                <td>${contract.amount?.toLocaleString()}</td>
                <td>{new Date(contract.startDate).toLocaleDateString()}</td>
                <td>{new Date(contract.endDate).toLocaleDateString()}</td>
                <td>
                  <span className={`status-badge ${contract.status?.toLowerCase()}`}>
                    {contract.status}
                  </span>
                </td>
                <td>
                  <span className={`approval-badge ${contract.approvalStatus?.toLowerCase()}`}>
                    {contract.approvalStatus}
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

export default Contracts;
