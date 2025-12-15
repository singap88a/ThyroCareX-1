import React from 'react';

function DiaryList({ entries, onDelete }) {
    if (!entries || entries.length === 0) {
        return <p style={{ color: '#888' }}>No entries yet. Start writing!</p>;
    }

    return (
        <div className="entry-list">
            {entries.map((entry) => (
                <div key={entry.id} className="entry">
                    <div className="entry-header">
                        <span>{new Date(entry.date).toLocaleString()}</span>
                        <button
                            className="delete-btn"
                            onClick={() => onDelete(entry.id)}
                        >
                            Delete
                        </button>
                    </div>
                    <p>{entry.content}</p>
                </div>
            ))}
        </div>
    );
}

export default DiaryList;
