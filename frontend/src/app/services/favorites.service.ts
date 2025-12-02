import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../../environments/environment';

@Injectable({ providedIn: 'root' })
export class FavoritesService {
  private http = inject(HttpClient);
  private favorites = new Set<string>();
  private favoritesSubject = new BehaviorSubject<Set<string>>(new Set());

  favorites$ = this.favoritesSubject.asObservable();

  initialize(ids: string[] = []) {
    this.favorites = new Set(ids || []);
    this.emit();
  }

  toggle(id: string): Observable<boolean> {
    if (!id) throw new Error('ID is required');

    return this.http.put<boolean>(`${environment.apiUrl}/api/cvs/togglefavorited/${id}`, {}).pipe(
      tap((isFavorited) => {
        if (isFavorited) {
          this.favorites.add(id);
        } else {
          this.favorites.delete(id);
        }
        this.emit();
      })
    );
  }

  isFavorite(id: string): boolean {
    return this.favorites.has(id);
  }

  getCount(): number {
    return this.favorites.size;
  }

  private emit() {
    this.favoritesSubject.next(new Set(this.favorites));
  }
}
