import React, { useState, useEffect } from 'react';
import axios from 'axios';

interface Disease {
  id: string;
  name: string;
  description: string;
}

interface Doctor {
  id: string;
  fullName: string;
  specialty: string;
}

interface Patient {
  id: string;
  fullName: string;
  assignedDoctor?: Doctor;
  diagnosedDiseases?: Disease[];
}

const PatientManager: React.FC = () => {
  const [patients, setPatients] = useState<Patient[]>([]);
  const [selectedPatient, setSelectedPatient] = useState<Patient | null>(null);

  useEffect(() => {
    fetchPatients();
  }, []);

  const fetchPatients = async (): Promise<void> => {
    try {
      const response = await axios.get<Patient[]>('https://localhost:7556/api/Patients');
      setPatients(response.data);
      if (response.data.length > 0) {
        fetchPatientDetails(response.data[0].id);
      }
    } catch (error) {
      console.error('Error:', error);
    }
  };

  const fetchPatientDetails = async (patientId: string): Promise<void> => {
    try {
      const response = await axios.get<Patient>(`https://localhost:7556/api/Patients/${patientId}`);
      setSelectedPatient(response.data);
    } catch (error) {
      console.error('Error:', error);
    }
  };

  return (
    <div>
      <h2>Управление пациентами</h2>

      <div className="patient-grid">
        <div className="patient-list">
          <div className="card">
            <h3>Список пациентов ({patients.length})</h3>
            {patients.map(patient => (
              <div 
                key={patient.id} 
                className={`patient-card ${selectedPatient?.id === patient.id ? 'active' : ''}`}
                onClick={() => fetchPatientDetails(patient.id)}
              >
                <h4>{patient.fullName}</h4>
                <p>Врач: {patient.assignedDoctor?.fullName || 'Не назначен'}</p>
                <p>Заболеваний: {patient.diagnosedDiseases?.length || 0}</p>
              </div>
            ))}
          </div>
        </div>

        {selectedPatient && (
          <div className="patient-details card">
            <h3>Детали пациента</h3>
            
            <div style={{marginBottom: '25px'}}>
              <h4>{selectedPatient.fullName}</h4>
            </div>

            <div style={{marginBottom: '25px'}}>
              <h4>Лечащий врач</h4>
              {selectedPatient.assignedDoctor ? (
                <div style={{background: '#f8f9fa', padding: '15px', borderRadius: '8px'}}>
                  <p><strong>Имя:</strong> {selectedPatient.assignedDoctor.fullName}</p>
                  <p><strong>Специальность:</strong> {selectedPatient.assignedDoctor.specialty}</p>
                </div>
              ) : (
                <p style={{color: '#999'}}>Врач не назначен</p>
              )}
            </div>

            <div>
              <h4>Диагностированные заболевания ({selectedPatient.diagnosedDiseases?.length || 0})</h4>
              {selectedPatient.diagnosedDiseases && selectedPatient.diagnosedDiseases.length > 0 ? (
                <div style={{display: 'grid', gap: '10px'}}>
                  {selectedPatient.diagnosedDiseases.map(disease => (
                    <div key={disease.id} className="disease-item">
                      <h5>{disease.name}</h5>
                      <p style={{margin: 0}}>{disease.description}</p>
                    </div>
                  ))}
                </div>
              ) : (
                <p style={{color: '#999'}}>Нет диагностированных заболеваний</p>
              )}
            </div>
          </div>
        )}
      </div>
    </div>
  );
};

export default PatientManager;