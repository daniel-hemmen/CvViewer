import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment.development';
import { forkJoin } from 'rxjs';

interface Cv {
  id: string;
  [key: string]: any;
}

@Component({
  selector: 'app-dashboard',
  imports: [CommonModule, MatCardModule, MatIconModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss',
})
export class Dashboard implements OnInit {
  private http = inject(HttpClient);

  totalCvs: number | null = null;
  recentlyUpdated: number | null = null;
  favorites: number | null = null;

  ngOnInit(): void {
    this.loadSummary();
  }

  loadSummary(): void {
    const thirtyDaysAgo = new Date();
    thirtyDaysAgo.setDate(thirtyDaysAgo.getDate() - 30);
    const thirtyDaysAgoIso = thirtyDaysAgo.toISOString();

    forkJoin({
      totalCvs: this.http.get<number>(`${environment.apiUrl}/api/cvs/total`),
      favorites: this.http.get<number>(`${environment.apiUrl}/api/cvs/count/favorited`),
      recentlyUpdated: this.http.get<Cv[]>(
        `${environment.apiUrl}/api/cvs/count/updated-since/${thirtyDaysAgoIso}`
      ),
    }).subscribe({
      next: (results) => {
        this.totalCvs = results.totalCvs ?? 0;
        this.favorites = results.favorites ?? 0;
        this.recentlyUpdated = results.recentlyUpdated?.length ?? 0;
      },
      error: (err) => {
        console.error('Failed to load dashboard data', err);
        this.totalCvs = 0;
        this.recentlyUpdated = 0;
        this.favorites = 0;
      },
    });
  }
}
