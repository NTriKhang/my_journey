'use server'

interface StartSessionResponse {
  sessionId: string;
  startedAt: string;
}

interface StopSessionResponse {
  duration: number;
  stoppedAt: string;
}

export async function startSession(): Promise<StartSessionResponse> {
  const response = await fetch('http://localhost:5000/api/sessions/start', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({
      timestamp: new Date().toISOString(),
    }),
  });

  if (!response.ok) {
    throw new Error('Failed to start session');
  }

  return response.json();
}

export async function stopSession(
  sessionId: string,
  duration: number
): Promise<StopSessionResponse> {
  const response = await fetch(
    `http://localhost:5000/api/sessions/${sessionId}/stop`,
    {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        duration,
        timestamp: new Date().toISOString(),
      }),
    }
  );

  if (!response.ok) {
    throw new Error('Failed to stop session');
  }

  return response.json();
}
