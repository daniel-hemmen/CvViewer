import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideZonelessChangeDetection } from '@angular/core';
import { DetailView } from './detail-view';

describe('DetailView', () => {
  let component: DetailView;
  let fixture: ComponentFixture<DetailView>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DetailView],
      providers: [provideZonelessChangeDetection()],
    }).compileComponents();

    fixture = TestBed.createComponent(DetailView);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
