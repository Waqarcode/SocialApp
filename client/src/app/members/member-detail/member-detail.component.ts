import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxGalleryAnimation, NgxGalleryImage, NgxGalleryOptions } from '@kolkov/ngx-gallery';
import { Member } from 'src/app/_models/member';
import { MembersService } from 'src/app/_services/members.service';
// import { NgxGalleryOptions, NgxGalleryImage } from 'ngx-gallery';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit {

  member : Member;
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];

  constructor(private memberService: MembersService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.loadMemberDetail();
    this.galleryOptions = [
      {
        width: '500px',
        height: '500px',
        thumbnailsColumns: 4,
        imageAnimation: NgxGalleryAnimation.Slide,
        preview: false
      },
    ];
    // this.galleryImages = [
    //   {
    //     small: 'https://lukasz-galka.github.io/ngx-gallery-demo/assets/img/1-small.jpeg',
    //     medium: 'https://lukasz-galka.github.io/ngx-gallery-demo/assets/img/1-medium.jpeg',
    //     big: 'https://lukasz-galka.github.io/ngx-gallery-demo/assets/img/1-big.jpeg'
    //   },
    //   {
    //     small: 'https://lukasz-galka.github.io/ngx-gallery-demo/assets/img/2-small.jpeg',
    //     medium: 'https://lukasz-galka.github.io/ngx-gallery-demo/assets/img/2-medium.jpeg',
    //     big: 'https://lukasz-galka.github.io/ngx-gallery-demo/assets/img/2-big.jpeg'
    //   },
    //   {
    //     small: 'https://lukasz-galka.github.io/ngx-gallery-demo/assets/img/3-small.jpeg',
    //     medium: 'https://lukasz-galka.github.io/ngx-gallery-demo/assets/img/3-medium.jpeg',
    //     big: 'https://lukasz-galka.github.io/ngx-gallery-demo/assets/img/3-big.jpeg'
    //   },
    //   {
    //     small: 'https://lukasz-galka.github.io/ngx-gallery-demo/assets/img/4-small.jpeg',
    //     medium: 'https://lukasz-galka.github.io/ngx-gallery-demo/assets/img/4-medium.jpeg',
    //     big: 'https://lukasz-galka.github.io/ngx-gallery-demo/assets/img/4-big.jpeg'
    //   },
    //   {
    //     small: 'https://lukasz-galka.github.io/ngx-gallery-demo/assets/img/5-small.jpeg',
    //     medium: 'https://lukasz-galka.github.io/ngx-gallery-demo/assets/img/5-medium.jpeg',
    //     big: 'https://lukasz-galka.github.io/ngx-gallery-demo/assets/img/5-big.jpeg'
    //   }      
    // ];
  }
  loadMemberDetail(){
    this.memberService.getMember(this.route.snapshot.paramMap.get('username'))
    .subscribe(response => {
      this.member = response
      this.galleryImages = this.getImage();
    })
  }
  getImage(): NgxGalleryImage[]{
    const photoUrl = [];
    for (const p of this.member.photos) {
      photoUrl.push({
        small: p.url,
        "medium": p.url,
        "big": p.url,
      })
    }
    return photoUrl;
  }

}
