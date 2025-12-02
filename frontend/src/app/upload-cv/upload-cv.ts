import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatDividerModule } from '@angular/material/divider';
import { environment } from '../../environments/environment';

@Component({
  selector: 'app-upload-cv',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatCardModule,
    MatIconModule,
    MatProgressBarModule,
    MatSnackBarModule,
    MatDividerModule,
  ],
  templateUrl: './upload-cv.html',
  styleUrl: './upload-cv.scss',
})
export class UploadCv {
  private http = inject(HttpClient);
  private snackBar = inject(MatSnackBar);

  selectedFile = signal<File | null>(null);
  isUploading = signal<boolean>(false);

  downloadTemplate(): void {
    const url = `${environment.apiUrl}/api/upload/template`;

    window.open(url, '_blank');
  }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      const file = input.files[0];

      if (!file.name.endsWith('.xlsx')) {
        this.snackBar.open('Please select a valid .xlsx file', 'Close', { duration: 3000 });
        return;
      }
      this.selectedFile.set(file);
    }
  }

  uploadFile(): void {
    const file = this.selectedFile();
    if (!file) return;

    this.isUploading.set(true);
    const formData = new FormData();
    formData.append('file', file);

    this.http.post(`${environment.apiUrl}/api/upload`, formData).subscribe({
      next: () => {
        this.isUploading.set(false);
        this.selectedFile.set(null);
        this.snackBar.open('Upload successful!', 'Close', { duration: 3000 });
      },
      error: (err) => {
        this.isUploading.set(false);
        console.error('Upload failed', err);
        this.snackBar.open('Upload failed. Please try again.', 'Close', { duration: 3000 });
      },
    });
  }
}
