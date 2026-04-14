using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practica_hilo_unitario
{
    internal class Class2
    {
            // ── Lógica del hilo: carga de imágenes ───────────────────────────
            private void CargarImagenes()
            {
                for (int i = 0; i < rutasImagenes.Length; i++)
                {
                    // ¿Se pidió detener?
                    if (detenido) break;

                    // ¿Está suspendido? → esperar
                    lock (lockPausa)
                    {
                        while (pausado && !detenido)
                            Monitor.Wait(lockPausa);
                    }

                    if (detenido) break;

                    // Cargar imagen (puede lanzar excepción → mostrar placeholder)
                    Image img = null;
                    try { img = Image.FromFile(rutasImagenes[i]); }
                    catch { img = null; }

                    // Capturar índice e imagen para el closure
                    int idx = i;
                    Image imagenFin = img;

                    // Actualizar UI en el hilo principal
                    this.Invoke((Action)(() =>
                    {
                        if (imagenFin != null)
                            pictureBoxes[idx].Image = imagenFin;
                        else
                            pictureBoxes[idx].Invalidate(); // muestra placeholder
                        progressBar.Value = idx + 1;
                    }));

                    /* Pequeña pausa para visualizar la carga progresiva
                    Thread.Sleep(800);
                }

                // Hilo terminó su trabajo → STOPPED
                if (!detenido)
                {
                    this.Invoke((Action)(() =>
                    {
                        ActualizarEstadoLabel("STOPPED", 3);
                        btnSuspender.Enabled = false;
                        btnReanudar.Enabled = false;
                        btnDetener.Enabled = false;
                        btnIniciar.Enabled = true;
                    }));
                }
            }

            // ── Actualizar label y badges ────────────────────────────────────
            private void ActualizarEstadoLabel(string estado, int indice)
            {
                // Este método se llama desde el hilo principal o mediante Invoke
                if (this.InvokeRequired)
                {
                    this.Invoke((Action)(() => ActualizarEstadoLabel(estado, indice)));
                    return;
                }

                string[] iconos = { "⏸", "▶", "⏯", "⏹" };
                lblEstado.Text = $"Estado actual del hilo:  {iconos[indice]} {estado}";
                lblEstado.ForeColor = COLORES_ESTADO[indice];

                // Resaltar badge activo, opacar los demás
                for (int i = 0; i < 4; i++)
                {
                    lblEstadosBadge[i].BackColor = (i == indice)
                        ? COLORES_ESTADO[i]
                        : Color.FromArgb(50, 50, 70);
                    lblEstadosBadge[i].ForeColor = (i == indice)
                        ? Color.Black
                        : Color.FromArgb(140, 140, 180);
                }
            }

            // ── Limpiar galería ──────────────────────────────────────────────
            private void LimpiarGaleria()
            {
                foreach (var pb in pictureBoxes)
                {
                    pb.Image?.Dispose();
                    pb.Image = null;
                    pb.Invalidate();
                }
            }

            // ── Cierre del form ──────────────────────────────────────────────
            protected override void OnFormClosing(FormClosingEventArgs e)
            {
                detenido = true;
                pausado = false;
                lock (lockPausa) Monitor.PulseAll(lockPausa);
                hiloGaleria?.Join(500);
                base.OnFormClosing(e);
            }

    }
}

*/