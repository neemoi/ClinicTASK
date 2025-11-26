import React, { useState } from 'react';
import './App.css';
import DiseaseManager from './components/DiseaseManager.tsx';
import DoctorManager from './components/DoctorManager.tsx';
import PatientManager from './components/PatientManager.tsx';

function App() {
  const [activeTab, setActiveTab] = useState('diseases');

  return (
    <div className="App">
      <div className="header">
        <h1>Медицинская система</h1>
        <div className="tabs">
          <button 
            className={activeTab === 'diseases' ? 'active' : ''} 
            onClick={() => setActiveTab('diseases')}
          >
            Заболевания
          </button>
          <button 
            className={activeTab === 'doctors' ? 'active' : ''} 
            onClick={() => setActiveTab('doctors')}
          >
            Врачи
          </button>
          <button 
            className={activeTab === 'patients' ? 'active' : ''} 
            onClick={() => setActiveTab('patients')}
          >
            Пациенты
          </button>
        </div>
      </div>

      <div className="content">
        {activeTab === 'diseases' && <DiseaseManager />}
        {activeTab === 'doctors' && <DoctorManager />}
        {activeTab === 'patients' && <PatientManager />}
      </div>
    </div>
  );
}

export default App;