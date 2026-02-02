export const API_BASE = process.env.NEXT_PUBLIC_API_BASE_URL ?? '';

export async function fetcher(url: string) {
  const res = await fetch(url);
  if (!res.ok) throw new Error('Network error');
  return res.json();
}

async function request(path: string, options?: RequestInit) {
  const res = await fetch(`${API_BASE}${path}`, options);
  if (!res.ok) {
    const text = await res.text().catch(() => null);
    throw new Error(text || `${res.status} ${res.statusText}` || 'Network error');
  }
  if (res.status === 204) return null;
  return res.json();
}

export function listLearningSessions() {
  return request('/api/learningsessions');
}

export function getLearningSession(id: string) {
  return request(`/api/learningsessions/${id}`);
}

export function startLearningSession(payload: { id?: string | null; startedAt: string; activityIds?: string[] | null }) {
  return request('/api/learningsessions', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(payload),
  });
}

export function endLearningSession(id: string, payload: { endedAt: string }) {
  return request(`/api/learningsessions/${id}/end`, {
    method: 'PUT',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(payload),
  });
}

export function addActivity(id: string, activityId: string) {
  return request(`/api/learningsessions/${id}/activities`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ activityId }),
  });
}

export function removeActivity(id: string, activityId: string) {
  return request(`/api/learningsessions/${id}/activities/${activityId}`, {
    method: 'DELETE',
  });
}

