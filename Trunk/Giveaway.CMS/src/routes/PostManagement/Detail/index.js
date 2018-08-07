import React from 'react';
import { connect } from 'dva';
import { routerRedux } from 'dva/router';
import { Button, Popconfirm } from 'antd';

@connect(({ modals, postManagement }) => ({
  ...modals, ...postManagement,
}))
export default class index extends React.Component {
  componentDidMount() {
    const { posts, dispatch } = this.props;
    if (posts.length === 0) {
      dispatch({
        type: 'postManagement/fetch',
        payload: {},
      });
    }
  }

  navigateToEditPage = () => {
    const { dispatch, posts, match: { params }, history } = this.props;
    dispatch({
      type: 'postManagement/deletePost',
      payload: {
        id: params.id,
        posts,
      },
    });
    dispatch(routerRedux.push('/post-management'));
  }

  render() {
    const { posts } = this.props;
    const PostNo = this.props.match.params.id;
    const result = posts ? posts.filter((val) => {
      return val.postId === PostNo;
    }) : [];
    return (result[0])
      ? (
        <div>
          <div className="containerHeader">
            <h1>Chi tiết bài đăng</h1>
            <div className="rightButton">
              <Popconfirm
                title="Bạn chắc chắn muốn xóa?"
                onConfirm={() => this.navigateToEditPage()}
              >
                <Button type="primary">
                  Ẩn Post
                </Button>
              </Popconfirm>
            </div>

          </div>
          <div className="containerBody">
            <h3> Post Id        : {result[0].postId}</h3>
            <h3> Tiêu đề        : {result[0].title} </h3>
            <h3> Mô tả          : {result[0].description} </h3>
            <h3> Địa chỉ        : {result[0].postAddress} </h3>
            <h3> Category       : {result[0].category} </h3>
            <h3> User           : {result[0].userPost} </h3>
            <div>
              {(result[0].imagePaths)
                ?
                result[0].imagePaths.map((imagePath) => {
                  return <img src={imagePath} key={imagePath} alt="" height="300" width="300" />;
                })

                : null}
            </div>
          </div>
        </div>
      ) : null;
  }
}
