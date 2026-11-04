const CLAVE_TEMA = 'tienda-tema';

export function obtenerTema() {
    try {
        const guardado = localStorage.getItem(CLAVE_TEMA);
        if (guardado) return guardado;
    } catch { /* localStorage no disponible */ }
    return window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light';
}

export function aplicarTema(tema) {
    document.documentElement.setAttribute('data-bs-theme', tema);
    try { localStorage.setItem(CLAVE_TEMA, tema); } catch { }
}

export async function copiarAlPortapapeles(texto) {
    await navigator.clipboard.writeText(texto);
}
export async function mostrarNotificacion(titulo, mensaje) {
    if (!('Notification' in window)) return 'no-soportado';

    let permiso = Notification.permission;
    if (permiso === 'default') {
        permiso = await Notification.requestPermission();
    }
    if (permiso === 'granted') {
        new Notification(titulo, { body: mensaje });
    }
    return permiso;
}