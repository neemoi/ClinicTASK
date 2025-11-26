import React, { useState, useEffect } from 'react';
import axios from 'axios';

interface Disease {
  id: string;
  name: string;
  description: string;
}

interface FormData {
  name: string;
  description: string;
}

const DiseaseManager: React.FC = () => {
  const [diseases, setDiseases] = useState<Disease[]>([]);
  const [editingDisease, setEditingDisease] = useState<Disease | null>(null);
  const [formData, setFormData] = useState<FormData>({ name: '', description: '' });

  useEffect(() => {
    fetchDiseases();
  }, []);

  const fetchDiseases = async (): Promise<void> => {
    try {
      const response = await axios.get<Disease[]>('https://localhost:7556/api/Diseases');
      setDiseases(response.data);
    } catch (error) {
      console.error('Error:', error);
    }
  };

  const handleSubmit = async (e: React.FormEvent): Promise<void> => {
    e.preventDefault();
    try {
      if (editingDisease) {
        const requestData = {
          id: editingDisease.id,
          name: formData.name,
          description: formData.description
        };
        await axios.put('https://localhost:7556/api/Diseases', requestData);
      }
      setFormData({ name: '', description: '' });
      setEditingDisease(null);
      fetchDiseases();
    } catch (error) {
      console.error('Error:', error);
    }
  };

  const handleEdit = (disease: Disease): void => {
    setEditingDisease(disease);
    setFormData({ name: disease.name, description: disease.description });
  };

  const cancelEdit = (): void => {
    setEditingDisease(null);
    setFormData({ name: '', description: '' });
  };

  return (
    <div>
      <h2>Управление заболеваниями</h2>
      
      {editingDisease && (
        <div className="card">
          <h3>Редактировать заболевание</h3>
          <form onSubmit={handleSubmit}>
            <div className="form-group">
              <label>Название заболевания:</label>
              <input
                type="text"
                value={formData.name}
                onChange={(e) => setFormData({...formData, name: e.target.value})}
                required
                placeholder="Введите название"
              />
            </div>
            <div className="form-group">
              <label>Описание:</label>
              <textarea
                value={formData.description}
                onChange={(e) => setFormData({...formData, description: e.target.value})}
                rows={3}
                required
                placeholder="Введите описание"
              />
            </div>
            <button type="submit" className="btn btn-primary">
              Сохранить изменения
            </button>
            <button 
              type="button" 
              className="btn btn-secondary"
              onClick={cancelEdit}
            >
              Отмена
            </button>
          </form>
        </div>
      )}

      <div className="card">
        <h3>Список заболеваний ({diseases.length})</h3>
        <div style={{display: 'grid', gap: '15px'}}>
          {diseases.map(disease => (
            <div key={disease.id} className="disease-item">
              <div style={{display: 'flex', justifyContent: 'space-between', alignItems: 'start'}}>
                <div style={{flex: 1}}>
                  <h5>{disease.name}</h5>
                  <p style={{color: '#666', margin: 0}}>{disease.description}</p>
                </div>
                <div style={{display: 'flex', gap: '10px'}}>
                  <button 
                    className="btn btn-primary btn-small"
                    onClick={() => handleEdit(disease)}
                  >
                    Редактировать
                  </button>
                </div>
              </div>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
};

export default DiseaseManager;