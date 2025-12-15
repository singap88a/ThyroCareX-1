import React, { useState } from 'react';

function EntryForm({ onEntryAdded }) {
    const [content, setContent] = useState('');
    const [isSubmitting, setIsSubmitting] = useState(false);

    const handleSubmit = async (e) => {
        e.preventDefault();
        if (!content.trim()) return;

        setIsSubmitting(true);
        try {
            const response = await fetch('http://localhost:5000/api/diary', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ content }),
            });

            if (response.ok) {
                setContent('');
                if (onEntryAdded) onEntryAdded();
            } else {
                alert('Failed to save entry');
            }
        } catch (error) {
            console.error('Error adding entry:', error);
            alert('Error adding entry');
        } finally {
            setIsSubmitting(false);
        }
    };

    return (
        <form onSubmit={handleSubmit} className="card">
            <textarea
                value={content}
                onChange={(e) => setContent(e.target.value)}
                placeholder="What's on your mind today?"
                rows={4}
            />
            <button type="submit" disabled={isSubmitting || !content.trim()}>
                {isSubmitting ? 'Saving...' : 'Save Entry'}
            </button>
        </form>
    );
}

export default EntryForm;
