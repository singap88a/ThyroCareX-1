import { useState, useEffect } from 'react';
import EntryForm from './components/EntryForm';
import DiaryList from './components/DiaryList';

function App() {
  const [entries, setEntries] = useState([]);

  const fetchEntries = async () => {
    try {
      const response = await fetch('http://localhost:5000/api/diary');
      if (response.ok) {
        const data = await response.json();
        setEntries(data);
      }
    } catch (error) {
      console.error('Error fetching entries:', error);
    }
  };

  const deleteEntry = async (id) => {
    if (!confirm('Are you sure you want to delete this entry?')) return;
    try {
      const response = await fetch(`http://localhost:5000/api/diary/${id}`, {
        method: 'DELETE',
      });
      if (response.ok) {
        fetchEntries();
      }
    } catch (error) {
      console.error('Error deleting entry:', error);
    }
  };

  useEffect(() => {
    fetchEntries();
  }, []);

  return (
    <div className="container">
      <h1>Daily Diary</h1>
      <EntryForm onEntryAdded={fetchEntries} />
      <hr style={{ margin: '2rem 0', borderColor: '#444' }} />
      <DiaryList entries={entries} onDelete={deleteEntry} />
    </div>
  );
}

export default App;
