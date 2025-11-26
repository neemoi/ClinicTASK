import React, { useState, useEffect } from 'react';
import axios from 'axios';

interface Patient {
  id: string;
  fullName: string;
}

interface Doctor {
  id: string;
  fullName: string;
  specialty: string;
  patients?: Patient[];
}

const DoctorManager: React.FC = () => {
  const [doctors, setDoctors] = useState<Doctor[]>([]);
  const [specialty, setSpecialty] = useState<string>('');

  useEffect(() => {
    fetchDoctors();
  }, []);

  const fetchDoctors = async (): Promise<void> => {
    try {
      const response = await axios.get<Doctor[]>('https://localhost:7556/api/Doctors');
      setDoctors(response.data);
    } catch (error) {
      console.error('Error:', error);
    }
  };

  const fetchDoctorsBySpecialty = async (): Promise<void> => {
    if (!specialty.trim()) {
      fetchDoctors();
      return;
    }
    try {
      const encodedSpecialty = encodeURIComponent(specialty);
      const response = await axios.get<Doctor[]>(`https://localhost:7556/api/Doctors/specialty/${encodedSpecialty}`);
      setDoctors(response.data);
    } catch (error) {
      console.error('Error:', error);
    }
  };

  const handleKeyPress = (e: React.KeyboardEvent): void => {
    if (e.key === 'Enter') {
      fetchDoctorsBySpecialty();
    }
  };

  return (
    <div>
      <h2>Управление врачами</h2>
      
      <div className="search-box">
        <input
          type="text"
          value={specialty}
          onChange={(e) => setSpecialty(e.target.value)}
          onKeyPress={handleKeyPress}
          placeholder="Введите специальность (например: Кардиология)"
        />
        <button className="btn btn-primary" onClick={fetchDoctorsBySpecialty}>
          Найти по специальности
        </button>
        <button className="btn btn-secondary" onClick={fetchDoctors}>
          Все врачи
        </button>
      </div>

      <div style={{display: 'grid', gap: '20px'}}>
        {doctors.map(doctor => (
          <div key={doctor.id} className="doctor-card">
            <div style={{display: 'flex', justifyContent: 'space-between', alignItems: 'start'}}>
              <div style={{flex: 1}}>
                <h3>{doctor.fullName}</h3>
                <p style={{fontSize: '18px', color: '#667eea', fontWeight: '600'}}>
                  {doctor.specialty}
                </p>
                
                <div style={{marginTop: '15px'}}>
                  <strong>Пациенты ({doctor.patients?.length || 0}):</strong>
                  <div style={{marginTop: '10px'}}>
                    {doctor.patients && doctor.patients.length > 0 ? (
                      doctor.patients.map(patient => (
                        <span key={patient.id} className="patient-badge">
                          {patient.fullName}
                        </span>
                      ))
                    ) : (
                      <p style={{color: '#999', fontStyle: 'italic'}}>Нет пациентов</p>
                    )}
                  </div>
                </div>
              </div>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};

export default DoctorManager;